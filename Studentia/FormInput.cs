namespace Studentia;

public partial class FormInput : Form
{
    // ----- Studentia: keep track of students grades -----

    // FormInput
    /* Index
     * - Global variables
     * - ctor
     * - Load Methods
     * - Controls' Methods
     * - Custom Methods
    */

    // --- Global Variables ---
    #region Global Variables

    Form1 _form1;
    char _action; // Either Search, Add Student or Modify
    int _foundQty = 0; // If Students found in Search, how many?

    int _number;
    string _name = "";
    float _grade;

    // Default Grade value for nudGrade
    readonly decimal DefaultGradeValue = 10;

    #endregion

    // --- ctor ---
    public FormInput(Form1 f, char a)
    {
        InitializeComponent();

        _form1 = f;
        _action = a;

        lblRecentEvents.ResetText();
        LoadForm();
    }

    // --- Methods ---

    #region Load Methods

    void LoadForm()
    {
        // Load Form for Search or Add Student

        switch (_action)
        {
            case 's': LoadFormSearch(); break;
            case 'a': LoadFormAddStudent(); break;
            case 'm': LoadFormModify(); break;
            default:
                MessageBox.Show("Something went wrong\n" +
                    "No valid Action was set for this Form.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                break;
        }
    }

    void LoadFormSearch()
    {
        Text = "Search";
        grbStudent.Text = "Search Student";
        btnAction.Text = "Search";
        chbName.Checked = true;
        txbName.Select();
    }

    void LoadFormAddStudent()
    {
        Text = "Add Studs";
        grbStudent.Text = "Add Student";
        btnAction.Text = "Add";

        // If FormInput _action is Add Student,
        // then all CheckBoxes are Checked and Disabled
        chbNumber.Checked = true;
        chbNumber.Enabled = false;
        chbName.Checked = true;
        chbName.Enabled = false;
        chbGrade.Checked = true;
        chbGrade.Enabled = false;

        // Get default value for nudNumber if Students exist in Form1
        if (_form1._students.Count > 0)
            nudNumber.Value = _form1._students.Max(s => s.Number) + 1;

        txbName.Select();
    }

    void LoadFormModify()
    {
        // Load Form for Modify Student

        Text = "Modify";
        grbStudent.Text = "Modify";
        btnAction.Text = "Modify";

        // Get Selected Student's values
        string[] s = _form1.GetSelectedStudentAsString().Split(';');

        // If anything's gone wrong in Getting the Selected Student's values,
        // this Form Closes
        // (because default values are necessary when passing Modified Student to Form1)

        if (s.Length != 3) Close();
        if (!int.TryParse(s[0], out _number)) Close();
        _name = s[1];
        if (!float.TryParse(s[2], out _grade)) Close();

        // Get Selected Student's current values into the input Controls
        nudNumber.Value = _number;
        txbName.Text = _name;
        nudGrade.Value = (decimal)_grade;
    }

    #endregion

    #region Controls' Methods

    private void lblItem_Click(object sender, EventArgs e)
    {
        // CheckBoxes are UnChecked when clicking Labels (Number, Name and Grade)
        // These Labels are such (instead of CheckBox.Text) so that
        // when FormInput _action is Add Student (and therefore CheckBoxes are Disabled)
        // these Labels stay Enabled

        Label lbl = (Label)sender;

        // If FormInput Action is Add Student, CheckBoxes are not UnCheckable
        if (_action == 'a') return;

        // UnCheck CheckBox based on which Label was clicked
        switch (lbl.Name)
        {
            case "lblNumber":
                if (chbNumber.Checked) chbNumber.Checked = false;
                else chbNumber.Checked = true;
                break;
            case "lblName":
                if (chbName.Checked) chbName.Checked = false;
                else chbName.Checked = true;
                break;
            case "lblGrade":
                if (chbGrade.Checked) chbGrade.Checked = false;
                else chbGrade.Checked = true;
                break;
        }
    }

    private void fields_KeyPress_Enter(object sender, KeyPressEventArgs e)
    {
        // When pressing Enter in Input Control
        // make sure related CheckBox is Checked and
        // do Action (Search, Add Student or Modify)

        if (e.KeyChar == (char)Keys.Enter)
        {
            // Get sender's name. It's either nudNumber, txbName or nudGrade
            // Different Control Types require a general Type -> (Control)sender

            string name = "chb" + ((Control)sender).Name[3..];

            CheckBox? chb = (CheckBox?)Controls.Find(name, true).FirstOrDefault();
            if (chb == null) return;

            chb.Checked = true;
            btnAction_Click(sender, e);

            // Prevent annoying sound when pressing Enter
            e.Handled = true; // Prevents when Focusing TextBox
            e.KeyChar = (char)Keys.D0; // Prevents when Focusing NumericUpDown
        }
        else if (e.KeyChar == (char)Keys.Escape)
        {
            Close();

            // Prevent annoying sound when pressing Esc
            e.Handled = true; // Prevents when Focusing TextBox
            e.KeyChar = (char)Keys.D0; // Prevents when Focusing NumericUpDown
        }

        // If the Preventing Sound Code is put here, TextBox Input is rejected
    }

    private void btnAction_Click(object sender, EventArgs e)
    {
        // Do Action
        switch (_action)
        {
            case 's': SearchStudent(); break;
            case 'a': AddStudent(); break;
            case 'm': ModifyStudent(); break;
        }
    }

    #endregion

    #region Custom Methods

    void SearchStudent()
    {
        // Search Students
        // Based on how many items (Number, Name, Grade) are selected (Checked)
        /* Search order
         * 1st- Find3(): Look for all sets of 3 correspondences -> if found, return
         * 2nd- Find2(): Look for all sets of 2 correspondences -> if found, return
         * 3rd- Find1(): Look for all sets of 1 correspondence -> if found, return
         */

        int searchQty = 0; // how many items to search (3, 2, 1) based on Checked items
        _foundQty = 0; // reset at every search

        // Default values:
        // when trying to match these values, search cycle will continue;
        // based on whichever items are Checked,
        // these variables will hold the values to look for.
        _number = -1;
        _name = "";
        _grade = -1;

        // The result of the Search will be a Selection of Items in Form1's ListView,
        // so, before that, current Selection is cleared
        _form1.UnselectListItems();

        // Check at least 1 item is Checked (Number, Name, Grade)
        if (!chbNumber.Checked && !chbName.Checked && !chbGrade.Checked)
        {
            lblRecentEvents.Text = "Search wasn't done";
            MessageBox.Show(
                "You need to select at least 1 item for search.", "Search",
                MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            return;
        }

        // Determine how many items will be Searched
        foreach (CheckBox chb in grbStudent.Controls.OfType<CheckBox>())
        {
            // For each Checked CheckBox,
            // update the value to look for and increment searchQty.
            switch (chb.Name)
            {
                case "chbNumber":
                    if (chb.Checked)
                    {
                        _number = (int)nudNumber.Value;
                        searchQty++;
                    }
                    break;
                case "chbName":
                    if (chb.Checked)
                    {
                        _name = txbName.Text.ToLower();
                        searchQty++;
                    }
                    break;
                case "chbGrade":
                    if (chb.Checked)
                    {
                        _grade = (int)nudGrade.Value;
                        searchQty++;
                    }
                    break;
            }
        }

        // Search progressively based on how many items are Checked
        // If found, stop Searching - Students found will be Selected in Form1's ListView

        // Look for 3 items
        if (searchQty == 3 && Find3()) return;

        // Look for 2 items
        if (searchQty > 1 && Find2()) return;

        // Look for 1 item
        if (Find1()) return;

        // If it gets here, No Student was Found
        lblRecentEvents.Text = "None found";
        _form1.Update_lblRecentEvents("Search student: none found");
    }

    bool Find3()
    {
        // Search Student - Look for 3 correspondences (item && item && item)
        // Select found Students, in Form1's ListView
        // If none is found, Method Find2() will be called

        bool found = false;

        foreach (Form1.Student s in _form1._students)
        {
            if (s.Number == _number &&
                _name != "" && s.Name.ToLower().Contains(_name) &&
                s.Grade == _grade)
            {
                _form1.SelectSearched(_form1._students.IndexOf(s));
                _foundQty++;
                found = true;
            }
        }
        if (found)
        {
            lblRecentEvents.Text = "Found " + _foundQty;
            _form1.Update_lblRecentEvents("Found " + _foundQty);
        }
        return found;
    }

    bool Find2()
    {
        // Search Student - Look for any set of 2 correspondences
        // Select found Students, in Form1's ListView
        // If none is found, Method Find1() will be called

        int found2;
        bool found = false;

        foreach (Form1.Student s in _form1._students)
        {
            found2 = 0;
            if (s.Number == _number) found2++;
            if (_name != "" && s.Name.ToLower().Contains(_name)) found2++;
            if (s.Grade == _grade) found2++;

            if (found2 == 2)
            {
                _form1.SelectSearched(_form1._students.IndexOf(s));
                _foundQty++;
                found = true;
            }
        }
        if (found)
        {
            lblRecentEvents.Text = "Found " + _foundQty;
            _form1.Update_lblRecentEvents("Found " + _foundQty);
        }
        return found;
    }

    bool Find1()
    {
        // Search Student - Look for any correspondence
        // Select found Students, in Form1's ListView

        bool found = false;
        foreach (Form1.Student s in _form1._students)
        {
            if (s.Number == _number ||
                (_name != "" && s.Name.ToLower().Contains(_name)) ||
                s.Grade == _grade)
            {
                _form1.SelectSearched(_form1._students.IndexOf(s));
                _foundQty++;
                found = true;
            }
        }
        if (found)
        {
            lblRecentEvents.Text = "Found " + _foundQty;
            _form1.Update_lblRecentEvents("Found " + _foundQty);
        }
        return found;
    }

    void AddStudent()
    {
        // Add Student to Form1.List<Student>
        // Only Name needs verification,
        // because Number and Grade come from NumericUpDown,
        // so their values will be of Type decimal.
        // The user is free to user whatever characters finds worth it,
        // except for semicolon (;), which is used in Saving and Opening Files
        // A MessageBox will be Shown to confirm the data of the Student to Add

        string[] strudent =
        {
            nudNumber.Value.ToString(),
            txbName.Text,
            nudGrade.Value.ToString("F2")
        };

        // Check there is a Name given
        if (strudent[1] == "")
        {
            MessageBox.Show(
                "A Name is needed.", "Add Student",
                MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            txbName.Focus();
            return;
        }

        // Check Name doesn't Contain ';'
        // (';' is used to Split when Saving and Opening Files)
        if (strudent[1].Contains(';'))
        {
            MessageBox.Show(
                "Name can't contain the character ; (semicolon).", "Add Student",
                MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            txbName.Focus();
            return;
        }

        // Confirm the user wants to Add this Student
        DialogResult addStud = MessageBox.Show(
            "This student will be added to the Class:\n" +
            "\n-Number: " + strudent[0] +
            "\n-Name: " + strudent[1] +
            "\n-Grade: " + strudent[2] +
            "\n\nContinue?",
            "Add Student",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question
            );

        // If user cancels, Method returns
        if (addStud == DialogResult.Cancel)
        {
            lblRecentEvents.Text = "Add Student: canceled";
            return;
        }

        // Add Student to the List<Student> in Form1
        _form1.AddStudent(strudent);

        // Prepare Form's input Controls for next Student to Add
        nudNumber.Value++;
        txbName.ResetText();
        nudGrade.Value = DefaultGradeValue;
        lblRecentEvents.Text = "New student";

        txbName.Focus();
    }

    void ModifyStudent()
    {
        // Modify Student
        // Default Student's values are those passed as argument string when
        // creating an instance of this Form
        // Each of these values is updated if the related CheckBox is Checked

        // Prepare string with current values for final MessageBox
        string[] startingValues =
        {
            _number.ToString(),
            _name,
            _grade.ToString("F2")
        };
        
        string previous = $"{_number} {_name} {_grade:F2}";
        string[] updated = new string[3];

        // Update variables' values based on Checked CheckBoxes
        if (chbNumber.Checked) _number = (int)nudNumber.Value;
        if (chbName.Checked) _name = txbName.Text;
        if (chbGrade.Checked) _grade = (float)nudGrade.Value;

        // Check Name doesn't Contain ';'
        // (';' is used to Split when Saving and Opening Files)
        if (CheckNameContainsSemicolon())
        {
            // Reset Input values to default (from Form1's Selected Student)
            ResetStartingValues(startingValues);
            return;
        }

        // The updated Student
        updated[0] = _number.ToString();
        updated[1] = _name;
        updated[2] = _grade.ToString("F2");

        // Confirm current and updated values
        DialogResult confirm = MessageBox.Show(
            "Are you sure you want to update this student:" +
            "\n" + previous +
            "\nwith these values:" +
            $"\n{updated[0]} {updated[1]} {updated[2]}",
            "Modify", MessageBoxButtons.OKCancel, MessageBoxIcon.Question
            );

        // If user has pressed button Cancel, return
        if (confirm == DialogResult.Cancel)
        {
            // Reset Input values to default (from Form1's Selected Student)
            ResetStartingValues(startingValues);
            return;
        }

        // Update Student in Form1
        _form1.ModifyStudent(updated);

        // Changes are made, Close this Form
        Close();
    }

    bool CheckNameContainsSemicolon()
    {
        // If Name Contains Semicolon, return true
        if (_name.Contains(';'))
        {
            MessageBox.Show(
                "Name can't contain the character ; (semicolon).", Text,
                MessageBoxButtons.OK, MessageBoxIcon.Information
                );

            txbName.Focus();
            return true;
        }
        return false;
    }

    void ResetStartingValues(string[] sv)
    {
        // This will most likely go well, because all variables have been treated before
        // try/catch is used to make sure the program won't break on Parsing

        try
        {
            _number = int.Parse(sv[0]);
            _name = sv[1];
            _grade = float.Parse(sv[2]);
        }
        catch
        {
            MessageBox.Show("Something went wrong.", $"Error: {_action}");
        }
    }

    #endregion

}
