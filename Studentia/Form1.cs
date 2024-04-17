namespace Studentia;

public partial class Form1 : Form
{
    // ----- Studentia: keep track of students grades -----

    // Form1
    /* Index
     * - struct Student - (definition)
     * - Global variables
     * - ctor
     * - Custom Methods
     * - Controls' Methods
     * - public Methods
    */

    // --- struct Student ---
    public struct Student
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public float Grade { get; set; }

        public Student(int number, string name, float grade)
        {
            Number = number;
            Name = name;
            Grade = grade;
        }
    }

    // --- Global Variables ---
    #region Global Variables

    // List as base for ListView Items
    public List<Student> _students = new();

    // Global Variables
    FormInput? _frmI; // This allows for communication from this Form to the other
    string _path = "Studentia.txt"; // Path to save file
    string _fileName; // File Name without extension
    bool _saved = true; // Is the current file saved?
    bool _isWidthAdjusted = false; // Width of Column Name in ListView
    bool[] _isColumnOrdered = new bool[3]; // Is each column in ListView ordered?
    // This declaration of Colors makes it easier to change in the whole code
    readonly Color SpecialColor_Best = Color.LimeGreen;
    readonly Color SpecialColor_Worst = Color.Red;

    #endregion

    // --- ctor ---
    public Form1()
    {
        InitializeComponent();

        _fileName = _path[..^4];

        lblFileName.Text = _fileName;
        lblRecentEvents.ResetText();

        StartListView();
        ImportList();
        DisplayList();
    }

    // --- Custom Methods ---
 
    #region List(View) Custom Methods

    void StartListView()
    {
        // Self-explanatory

        lsvStudents.View = View.Details;
        lsvStudents.GridLines = true;
        lsvStudents.FullRowSelect = true;

        lsvStudents.Columns.Add("Nr", 40);
        lsvStudents.Columns.Add("Name", lsvStudents.Width - 4 - 40 - 64); // *
        lsvStudents.Columns.Add("Grade", 64);

        // * Width is adjusted for VScrollBar in DisplayList() *
    }

    void ImportList()
    {
        string? line;
        string[] data;
        string name;
        int number;
        float grade;
        Student student = new(0, "", 0);

        // If file is not found -> Quit this Method
        if (!File.Exists(_path))
        {
            lblFileName.Text = "Studentia";
            return;
        }

        // Try to read the file -> If not possible, Quit this Method

        FileStream fs;
        try { fs = new(_path, FileMode.Open); }
        catch { return; }

        StreamReader sr;
        try { sr = new(fs); }
        catch
        {
            MessageBox.Show("Could not read the file.", "Error");
            return;
        }

        // Read File opened and Populate List of Students
        while ((line = sr.ReadLine()) != null)
        {
            // Data is expected to be stored as Lines of 3 strings separated by char ';'
            // Number ; Name ; Grade
            // If any line doesn't fit this criteria -> continue;

            data = line.Split(';');

            // Make sure this line is readable as expected
            if (data.Length != 3) continue;

            // Check every piece from current line
            if (!int.TryParse(data[0], out number)) continue;
            name = data[1];
            if (name == "") continue;
            if (!float.TryParse(data[2], out grade)) continue;

            // Conclude this line
            student.Number = number;
            student.Name = name;
            student.Grade = grade;

            _students.Add(student);
        }

        sr.Close();
        fs.Close();

        lblFileName.Text = _fileName;
        lblRecentEvents.Text = "Class opened: " + _path;

        // File just opened is for sure saved
        _saved = true;
    }

    void DisplayList()
    {
        string[] lvi = new string[3];

        // Resetting ListView
        cbbBest.Checked = false;
        cbbWorst.Checked = false;
        btnModify.Enabled = false;
        btnRemove.Enabled = false;
        lsvStudents.Items.Clear();

        // Get all Students in List<>
        foreach (Student s in _students)
        {
            lvi[0] = s.Number.ToString();
            lvi[1] = s.Name;
            lvi[2] = s.Grade.ToString("F2");

            // Add student to ListView
            lsvStudents.Items.Add(new ListViewItem(lvi));
        }

        // Enable or Disable buttons whether there are Items in the ListView
        if (lsvStudents.Items.Count == 0) ListExists(false);
        else ListExists(true);

        // Adjust middle Column (Name) Width in case there is a VScrollBar
        AdjustColumnName();

        // Labels for stats (Highest grade, Lowest grade, Average grade) need Update
        UpdateLabelsBestWorstAvg();
    }

    void ListExists(bool b)
    {
        // Enable/Disable buttons based on the existence of Items in ListView

        btnSearch.Enabled = b;
        cbbBest.Enabled = b;
        cbbWorst.Enabled = b;

        // This happens only if ListView is empty
        if (!b)
        {
            // Update Labels
            lblRecentEvents.Text = "List is empty";
            lblBestWorst.Text = "Highest\n\nLowest";
            lblAvg.Text = "Average";

            btnAddStuds.Select();
        }
    }

    void AdjustColumnName()
    {
        // The Column for Name needs adjustment for whether there is a VScrollBar
        // Here's an estimate for the need for VScrollBar
        // If needed, Column Name is narrower
        // If not, Column Name is regular

        // Auxiliary variables to estimate the need for the VScrollBar
        int headerHeight;
        int itemHeight;
        int listHeight = 0; // If there's no Items in ListView, List Height is == 0

        // This assessment is possible only if there's at least 1 Item in ListView
        if (lsvStudents.Items.Count > 0)
        {
            // List Height is equivalent to the height of: Header + Items
            headerHeight = lsvStudents.TopItem.Bounds.Top;
            itemHeight = lsvStudents.TopItem.Bounds.Height;
            listHeight = headerHeight + itemHeight * lsvStudents.Items.Count;
        }

        // Column Width may be adjusted and readjusted over and over.
        // (Adding/Removing students, Opening/Creating/Deleting file)
        // Hence, there's a need to keep track of the Adjustment and the need for it

        if (listHeight > lsvStudents.ClientSize.Height)
        {
            if (!_isWidthAdjusted)
            {
                lsvStudents.Columns[1].Width -= SystemInformation.VerticalScrollBarWidth;
                _isWidthAdjusted = true;
            }
        }
        else
        {
            if (_isWidthAdjusted)
            {
                lsvStudents.Columns[1].Width += SystemInformation.VerticalScrollBarWidth;
                _isWidthAdjusted = false;
            }
        }
    }

    void ColumnsAreNotOrdered()
    {
        // Whenever there's a change in the ListView, this Array is resetted

        for (int i = 0; i < 3; i++) _isColumnOrdered[i] = false;
    }

    #endregion

    #region Save and Exit Custom Methods

    bool MakeBackupCopy(string action)
    {
        // Make a Backup of the File (called when Saving)

        try { File.Copy(_path, "Backup_Studentia", true); }
        catch
        {
            // If Backup is needed but not made, Show MessageBox and return false
            MessageBox.Show(
                "Backup Copy was not saved. Please try again.", action,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning
                );
            lblRecentEvents.Text = action + " canceled.";
            return false;
        }

        return true;
    }

    bool CheckUserWantsToSaveFile(string action)
    {
        // If current file is not saved, ask if user wants to save
        // - Returns false if user canceled or if something went wrong

        if (!_saved)
        {
            DialogResult save = MessageBox.Show(
                "The current class isn't saved." +
                "\nDo you want to save it before moving to another one?",
                action + ": save current?",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question
                );

            if (save == DialogResult.Cancel) return false;
            if (save == DialogResult.Yes)
            {
                bool successSaving = SaveFileStudentia(false);
                if (!successSaving) return false;
            }
        }

        return true;
    }

    bool SaveFileStudentia(bool requireSaveFileAs)
    {
        // Save current file or Save As
        // (bool requireSaveFileAs is needed when user clicks SaveClassAs in Top Menu)
        // - Return true if everything went well
        // - Return false if something went wrong or Saving was canceled

        // Check whether it's needed to MakeBackupCopy or SaveFileAs
        if (File.Exists(_path))
        {
            // Try to make Backup Copy; if can't, MessageBox is Shown in that Method
            if (!MakeBackupCopy("Save")) return false;
        }
        else requireSaveFileAs = true;

        // Check if Save As is required
        if (requireSaveFileAs)
        {
            // Try SaveFileDialog; if can't, MessageBox is Shown in that Method
            if (!SaveFileAs()) return false;
        }

        // Try to save file
        try
        {
            // _path may have been updated if SaveFileAs was done

            using StreamWriter sw = new(_path, false);

            foreach (Student s in _students)
            {
                sw.WriteLine($"{s.Number};{s.Name};{s.Grade:F2}");
            }

            sw.Close();

            lblRecentEvents.Text = "File saved: " + _path;

            MessageBox.Show("File Saved", "Save",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch
        {
            // If couldn't save file, Show MessageBox and return
            MessageBox.Show(
                "It was not possible to save the file. Please try again.",
                "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
            return false;
        }

        _saved = true;
        lblFileName.Text = _fileName;

        return true;
    }

    bool SaveFileAs()
    {
        // sfd1 is a SaveFileDialog

        sfd1.FileName = _path;

        if (sfd1.ShowDialog() == DialogResult.OK)
        {
            _path = Path.GetFileName(sfd1.FileName);
            _fileName = Path.GetFileNameWithoutExtension(sfd1.FileName);
        }
        else
        {
            lblRecentEvents.Text = "Save Class as: canceled.";
            return false;
        }

        return true;
    }

    bool ConfirmExitAndClose()
    {
        // Confirm Exit and Save if required
        // Method returns false if:
        // - Exit is canceled
        // - Save went wrong or was canceled

        if (_saved)
        {
            DialogResult exit = MessageBox.Show(
                $"{_fileName} has no changes to be saved. Exit?", "Exit",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question
                );

            if (exit == DialogResult.Cancel) return false;
        }
        else
        {
            DialogResult save = MessageBox.Show(
            $"Save '{_fileName}' before exiting?", "Exit",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question
            );

            if (save == DialogResult.Cancel) return false;
            if (save == DialogResult.Yes)
            {
                // Try to save before Exiting. If can't, return without Close()
                bool successSaving = SaveFileStudentia(false);
                if (!successSaving) return false;
            }
        }

        return true;
    }

    #endregion

    // Update Labels, UnCheck Buttons, Locate Form, New File/Class
    #region Other Custom Methods

    void UpdateLabelsBestWorstAvg()
    {
        // Get the stats from the list and Update its related Labels

        string highest;
        string lowest;
        string avg;

        // If there's no Students, the Update is straight and simple
        if (_students.Count < 1)
        {
            lblBestWorst.Text = "Highest\n\nLowest";
            lblAvg.Text = "Average";
            return;
        }

        highest = _students.Max(item => item.Grade).ToString("F2");
        lowest = _students.Min(item => item.Grade).ToString("F2");
        avg = _students.Average(item => item.Grade).ToString("F2");

        lblBestWorst.Text =
            $"Highest" +
            $"\n{highest}" +
            $"\n" +
            $"\nLowest" +
            $"\n{lowest}";

        lblAvg.Text = $"Average:\n{avg}";
    }

    void UnCheckBestWorst()
    {
        // Best and Worst Buttons give color to related Items (highest and lowest grades)
        // When a Student is removed,
        // this Method checks if there's still any Item with a Special BackColor.
        // If not, Uncheck the related Button (Best, Worst)

        bool isThereAnyLeft; // Is there any ListView Item with a Special BackColor?
        string cbbName;

        // Run through all the CheckBox Buttons
        foreach (CheckBox cbb in Controls.OfType<CheckBox>())
        {
            cbbName = cbb.Name;

            // Select and Update the Checked buttons
            if ((cbbName == "cbbBest" || cbbName == "cbbWorst") && cbb.Checked)
            {
                isThereAnyLeft = false;

                // If there's still at least 1 Item in ListView with a Special BackColor,
                // keep button Checked. Else, Uncheck it.
                foreach (ListViewItem item in lsvStudents.Items)
                {
                    if (item.BackColor == SpecialColor_Best && cbbName == "cbbBest" ||
                        item.BackColor == SpecialColor_Worst && cbbName == "cbbWorst")
                    {
                        isThereAnyLeft = true;
                        break;
                    }
                }
                cbb.Checked = isThereAnyLeft;
            }
        }
    }

    void LocateFormI()
    {
        // Locate the Input Form before it's Shown
        // (just to the right or to the left of the ListView)
        // (higher if Bottom is outside the Screen.WorkingArea)

        if (_frmI == null) return;

        // Default x: to the right of the ListView:
        int x = Left + lsvStudents.Right;

        // Default y: aligned with the top of the ListView
        int y = RectangleToScreen(ClientRectangle).Top + lsvStudents.Top;

        // Current Screen's WorkingArea Rectangle
        Rectangle r = Screen.GetWorkingArea(this);

        // If FormInput goes out of the screen to the right:
        // -> put it to the left of the ListView
        if (x + _frmI.Width > r.Right)
            x -= lsvStudents.Width + _frmI.Width - 22; // 22 == Sum of 2 borders (2 Forms)

        // If FormInput goes down out of the screen:
        // -> raise it just enough to fit well
        if (y + _frmI.Height > r.Bottom)
            y = r.Bottom - _frmI.Height;

        _frmI.Location = new Point(x, y);
    }

    void NewClassStudentia()
    {
        // Come up with a name for New File
        // "Studentia" + n -> n in [1..9] or n = 0
        _path = "Studentia0.txt";
        for (int i = 0; i < 9; i++)
        {
            if (!File.Exists("Studentia" + (i + 1) + ".txt"))
            {
                _path = "Studentia" + (i + 1) + ".txt";
                break;
            }
        }
        _fileName = _path[..^4];

        lblFileName.Text = _fileName + "(!)";

        _students.Clear(); // Clears current List<>
        cbbBest.Checked = false;
        cbbWorst.Checked = false;
        DisplayList();

        // bool _save is set true so as to
        // prevent asking for SaveFile when exiting or opening file
        // before any change is made
        _saved = true;
        lblRecentEvents.Text = "New class: not saved yet";
    }

    #endregion


    // --- Controls' Methods

    #region MenuStrip1 Click Methods

    private void New_ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Menu button for Creating New List of Students

        // Check if the user wants to save current file
        // Returns false if something went wrong
        if (!CheckUserWantsToSaveFile("New")) return;

        NewClassStudentia();
    }

    private void Open_ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Top menu button for Opening existing file

        // Check if the user wants to save currrent file
        // Returns false if something went wrong
        if (!CheckUserWantsToSaveFile("Open")) return;

        if (ofd1.ShowDialog() == DialogResult.OK)
        {
            _path = Path.GetFileName(ofd1.FileName);
            _fileName = Path.GetFileNameWithoutExtension(ofd1.FileName);

            _students.Clear();
            ImportList();
            DisplayList();
        }
    }

    private void OpenDefault_ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Open the Default File

        // Check if the user wants to save current file
        // Returns false if something went wrong
        if (!CheckUserWantsToSaveFile("Open default")) return;

        _path = "Studentia.txt";
        _fileName = _path[..^4];

        _students.Clear();
        ImportList();
        DisplayList();
    }

    private void SaveClassAs_ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Make sure SaveFileDialog is Shown, then save
        // ('true' requires SaveFileDialog)

        SaveFileStudentia(true);
    }

    private void Delete_ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Delete current file

        // Check current File Exists; if not, return
        if (!File.Exists(_path))
        {
            MessageBox.Show("This Class isn't saved.", "Delete",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // Confirmation
        DialogResult delete = MessageBox.Show(
            "Are you sure you want to delete this Class?", "Delete Class",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning
            );

        // If confirmed, Delete
        if (delete == DialogResult.OK)
        {
            // Try to make Backup Copy
            // Returns false if something went wrong
            if (!MakeBackupCopy("Delete")) return;

            // Try to Delete File
            try
            {
                File.Delete(_path);

                MessageBox.Show(
                    $"File {Path.GetFileNameWithoutExtension(_path)} was deleted.",
                    "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information
                    );

                _path = "Studentia.txt";
                _fileName = "Studentia";

                _students.Clear();

                ImportList();
                DisplayList();
            }
            catch
            {
                MessageBox.Show("Couldn't delete file.", "Delete",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }

    #endregion

    #region Buttons' Click Methods

    private void cbbBestWorst_Click(object sender, EventArgs e)
    {
        // Change BackColor of Items in ListView whose Grade is either Highest or Lowest

        CheckBox cbb = (CheckBox)sender;
        Color specialColor;
        float grade;
        int found = 0;
        string best_worst = "";

        // Check whether user clicked Best or Worst button
        switch (cbb.Name)
        {
            case "cbbBest":
                grade = _students.Max(item => item.Grade);
                specialColor = SpecialColor_Best;
                best_worst = "best";
                break;
            case "cbbWorst":
                grade = _students.Min(item => item.Grade);
                specialColor = SpecialColor_Worst;
                best_worst = "worst";
                break;
            default: return;
        }

        // Check to realize if button is supposed being turned Checked or Unchecked
        if (cbb.Checked)
        {
            lsvStudents.SelectedIndices.Clear();

            // Go through all Items in ListView
            for (int i = 0; i < lsvStudents.Items.Count; i++)
            {
                // Check if Student's grade matches (Best/Worst)
                if (_students[i].Grade == grade)
                {
                    // Make changes in ListView accordingly
                    lsvStudents.Items[i].BackColor = specialColor;
                    lsvStudents.Items[i].ForeColor = Color.White;
                    lsvStudents.EnsureVisible(i);
                    found++;
                }
            }

            // Update lblRecentEvents: how many Items have had BackColor changed
            if (found > 0) lblRecentEvents.Text = $"Found {found} {best_worst} grades";
        }
        else // If unchecking button
        {
            // Go through all Items in ListView
            for (int i = 0; i < lsvStudents.Items.Count; i++)
            {
                // Check BackColor of Item is changed and Reset it
                if (lsvStudents.Items[i].BackColor == specialColor)
                {
                    lsvStudents.Items[i].BackColor = Color.White;
                    lsvStudents.Items[i].ForeColor = Color.Black;
                    found++;
                }
            }
            // Update lblRecentEvents: how many Items have had BackColor resetted
            if (found > 0) lblRecentEvents.Text = $"Resetted {found} {best_worst} grades";
        }

        // If all Items have same Grade, when Checking one Button, the other one Dechecks
        UnCheckBestWorst();
    }

    private void btns_OpenFormInput_Click(object sender, EventArgs e)
    {
        // Open FormInput -> Action in (Search, Add Student, Modify)
        char action;

        switch (((Button)sender).Name)
        {
            case "btnSearch": action = 's'; break;
            case "btnAddStuds": action = 'a'; break;
            case "btnModify":

                // Make sure there's an Item Selected in ListView
                if (lsvStudents.SelectedIndices.Count != 1) return;

                action = 'm'; break;
            default: return;
        }

        _frmI = new FormInput(this, action);
        LocateFormI();

        if (_frmI != null) _frmI.ShowDialog();
        else
        {
            MessageBox.Show(
                "Something has gone wrong opening FormInput -> Action.", $"Error: {action}",
                MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            return;
        }
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
        // Remove Selected Student(s) and Item(s)

        // Make sure there's at least 1 Item Selected
        int selected = lsvStudents.SelectedIndices.Count;
        if (selected < 1) return;

        // Prepare Confirmation MessageBox with Selected Items' SubItems
        string ThisOrTheseStudentS = ( selected == 1 ? "this student" : "these students" );
        string students_to_remove = "";

        // Add each Selected Student to the string
        foreach (ListViewItem item in lsvStudents.SelectedItems)
        {
            students_to_remove += $"\n" +
                $"{item.SubItems[0].Text} " +
                $"{item.SubItems[1].Text} " +
                $"{item.SubItems[2].Text}";
        }

        // Confirmation
        DialogResult remove = MessageBox.Show(
            $"Are you sure you want to remove {ThisOrTheseStudentS}?" +
            "\n" + students_to_remove,
            "Remove Students",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question
            );

        // If canceled, return
        if (remove == DialogResult.Cancel)
        {
            lblRecentEvents.Text = "Remove canceled";
            return;
        }

        // Removing Selected Students -> No Selected Student remaining -> Disable buttons
        btnModify.Enabled = false;
        btnRemove.Enabled = false;

        // Remove and count (count is needed)
        int removed = 0;
        foreach (int i in lsvStudents.SelectedIndices)
        {
            // « i-removed » is needed, else wrong Students/Items would be removed
            _students.RemoveAt(i - removed);
            lsvStudents.Items.RemoveAt(i - removed);
            removed++;
        }

        AdjustColumnName();
        UpdateLabelsBestWorstAvg();
        UnCheckBestWorst(); // Decheck button(s) Best/Worst if necessary

        lblRecentEvents.Text = $"Removed {removed} student(s)";

        _saved = false;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        // SaveFileStudentia checks if there's need for SaveFileDialog or not
        // If File Exists, save directly
        SaveFileStudentia(false);
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        // Confirmation and Save prompt are called on FormClosing
        Close();
    }

    #endregion

    // FormClosing, ListView - Column Click, SelectedIndexChanged, Unselect Items
    #region Other Controls' Methods

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Confirm user wants to Save File and Exit
        if (!ConfirmExitAndClose()) e.Cancel = true;
    }

    private void lsvStudents_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        // Order and Reorder List<> based on ListView Header Click
        // Then, Display ListView as Ordered/Reordered

        // Get which Column was clicked
        int columnIndex = e.Column;

        // Check if this is Ordering (1st time) or Reordering (Order backwards)
        if (!_isColumnOrdered[columnIndex])
        {
            // Complicated Ordering
            // columnIndex 0: Column Number
            // columnIndex 1: Column Name
            // columnIndex 2: Column Grade
            switch (columnIndex)
            {
                case 0:
                    _students = _students.
                        OrderBy(s => s.Number).
                        ThenBy(s => s.Name).
                        ThenByDescending(s => s.Grade).
                        ToList();
                    break;
                case 1:
                    _students = _students.
                        OrderBy(s => s.Name).
                        ThenByDescending(s => s.Grade).
                        ThenBy(s => s.Number).
                        ToList();
                    break;
                case 2:
                    _students = _students.
                        OrderByDescending(s => s.Grade).
                        ThenBy(s => s.Name).
                        ThenBy(s => s.Number).
                        ToList();
                    break;
            }

            // Update global bool[] _isColumnOrdered;
            ColumnsAreNotOrdered();
            _isColumnOrdered[columnIndex] = true;
        }
        else
        {
            // Complicated Ordering
            // columnIndex 0: Column Number
            // columnIndex 1: Column Name
            // columnIndex 2: Column Grade
            switch (columnIndex)
            {
                case 0:
                    _students = _students.
                        OrderByDescending(s => s.Number).
                        ThenByDescending(s => s.Name).
                        ThenByDescending(s => s.Grade).
                        ToList();
                    break;
                case 1:
                    _students = _students.
                        OrderByDescending(s => s.Name).
                        ThenByDescending(s => s.Number).
                        ThenByDescending(s => s.Grade).ToList();
                    break;
                case 2:
                    _students = _students.
                        OrderBy(s => s.Grade).
                        ThenBy(s => s.Name).
                        ThenBy(s => s.Number).ToList();
                    break;
            }

            // Update global bool[] _isColumnOrdered;
            // Reordered Column is considered not Ordered
            ColumnsAreNotOrdered();
        }

        DisplayList();
    }

    private void lsvStudents_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Get which and how many Items in ListView are Selected
        // so as to Enable/Disable buttons Modify and Remove accordingly

        int selected = lsvStudents.SelectedIndices.Count;

        if (selected > 0)
        {
            // Button Modify accepts only 1 Selected Student
            if (selected == 1) btnModify.Enabled = true;
            else btnModify.Enabled = false;

            btnRemove.Enabled = true;
        }
        else
        {
            btnModify.Enabled = false;
            btnRemove.Enabled = false;
        }
    }

    private void UnselectListViewItems_OnLabelsClick(object sender, EventArgs e)
    {
        // When user clicks any Label in Form1 (except HighestLowest and Avg)
        // ListView Selection is Cleared
        lsvStudents.SelectedIndices.Clear();
    }

    #endregion


    // --- public Methods ---

    #region public Methods
    // These Methods are used by FormInput

    public void Update_lblRecentEvents(string text)
    {
        lblRecentEvents.Text = text;
    }

    public void SelectSearched(int index)
    {
        // Select Items in ListView according to Search from FormInput
        // (Method is called for every Student found)

        lsvStudents.Items[index].Selected = true;
        lsvStudents.EnsureVisible(index);
    }

    public void UnselectListItems()
    {
        lsvStudents.SelectedIndices.Clear();
    }

    public void AddStudent(string[] s)
    {
        // Add Student from FormInput
        // (input data verified in FormInput)

        // Variables used to Add new Student to the List
        int newIndex = lsvStudents.Items.Count;
        int number;
        string name = s[1];
        float grade;

        // Parse Number and Grade as int and float
        if (!int.TryParse(s[0], out number)) return;
        if (!float.TryParse(s[2], out grade)) return;

        // Clear Selection in ListView - new Student will be Selected
        lsvStudents.SelectedIndices.Clear();

        // Add Student to the List<> and to the ListView
        _students.Add(new Student(number, name, grade));
        lsvStudents.Items.Add(new ListViewItem(s));

        // Select and see the new Student
        lsvStudents.EnsureVisible(newIndex);
        lsvStudents.Items[newIndex].Selected = true;

        ListExists(true);
        AdjustColumnName();
        UpdateLabelsBestWorstAvg();
        ColumnsAreNotOrdered();

        lblRecentEvents.Text =
            $"New student: {number} {name} {grade:F2}";

        _saved = false;
    }

    public string GetSelectedStudentAsString()
    {
        // Return string with values of Selected Student for FormInput -> Modify

        string[] s =
        {
            lsvStudents.SelectedItems[0].SubItems[0].Text,
            lsvStudents.SelectedItems[0].SubItems[1].Text,
            lsvStudents.SelectedItems[0].SubItems[2].Text,
        };

        return $"{s[0]};{s[1]};{s[2]}";
    }

    public void ModifyStudent(string[] s)
    {
        // Modify Student using values from FormInput
        // (input values verified in FormInput)

        // Make sure there's 1 Selected Student
        if (lsvStudents.SelectedIndices.Count != 1) return;

        // Make sure there's 3 strings in the array
        if (s.Length != 3) return;

        // Variables used to update Student in List<>
        int index = lsvStudents.SelectedIndices[0];
        int number;
        string name = s[1];
        float grade;

        // Parse Number and Grade as int and float
        if (!int.TryParse(s[0], out number)) return;
        if (!float.TryParse(s[2], out grade)) return;

        // Add Student to List<>
        _students[index] = new Student(number, name, grade);

        DisplayList();

        // Select and see Modified Student
        lsvStudents.Items[index].Selected = true;
        lsvStudents.EnsureVisible(index);

        lblRecentEvents.Text = $"Modified: {number} {name} {grade:F2}";

        _saved = false;
    }

    #endregion

}