using StructuralBase.Section;
using StructuralBase.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using AdvInputOutput3;
using AdvInputOutput3.Controls;

namespace Hendese.Controls
{
    /// <summary>
    /// Interaction logic for SectionPage.xaml
    /// </summary>
    public partial class SectionPage : Page, INotifyPropertyChanged
    {
        private SectionType _selectedSection;

        public SectionType SelectedSection
        {
            get { return _selectedSection; }
            set
            {
                _selectedSection = value;
                OnPropertyChanged("SelectedSection");
            }
        }

        private bool _resultSuccess = false;

        public bool ResultSuccess
        {
            get { return _resultSuccess; }
            set
            {
                _resultSuccess = value;
                OnPropertyChanged("ResultSuccess");
            }
        }


        InternalForces Force;
        SectionBase Section = null;
        StructuralBase.Design.Steel.SteelDesign Design;
        StructuralBase.Material.Steel Material;
        InputControl sectionControl;
        OutputControl outputControl;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public SectionPage()
        {
            InitializeComponent();
            this.Force = new InternalForces();

            //InputControl.Settings.UpdateDynamically = true;
            //OutputControl.Settings.UpdateDynamically = true;
            //OutputControl.Settings.Floating = 2;

            this.DataContext = this;
            //this.comboBox.ItemsSource = SittinSection.SectionBase.SectionGroups;
            this.comboBox.ItemsSource = Enum.GetValues(typeof(StructuralBase.Section.SectionType))
                .Cast<StructuralBase.Section.SectionType>();


            InputControl cont = (InputControl)ControlBase.Create(ControlTypes.Input);
            cont.SourceObject = this.Force;
            cont.InputChanged += Check;
            cont.Settings.UpdateDynamically = true;
            this.ForceInput.Children.Clear();
            this.ForceInput.Children.Add(cont);

            ControlBase.RegisterProperties<StructuralBase.Design.Steel.SteelDesign>(ControlTypes.Input, d => d.K, d => d.Lx,
                d => d.Ly, d => d.MA, d => d.MB, d => d.MC, d => d.MMax);


            this.expInternalForces.IsExpanded = true;
            this.expSection.IsExpanded = false;
        }

        #region Accordion

        private void expInternalForces_Expanded(object sender, RoutedEventArgs e)
        {
            this.expSection.IsExpanded = false;
        }

        private void expSection_Expanded(object sender, RoutedEventArgs e)
        {
            this.expInternalForces.IsExpanded = false;
        }

        #endregion

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Section = SectionBase.Create(this.SelectedSection);
            Section.CalculateSecProps();
            Material = new StructuralBase.Material.Steel(210000, 355, 70000);
            Design = (StructuralBase.Design.Steel.SteelDesign)Section.DesignSection(Material);

            sectionControl = (InputControl)ControlBase.Create(ControlTypes.Input);
            sectionControl.SourceObject = this.Section;
            sectionControl.InputChanged += Check;
            sectionControl.Settings.UpdateDynamically = true;
            

            this.SectionInput.Children.Clear();
            this.SectionInput.Children.Add(sectionControl);
            
            outputControl = (OutputControl)ControlBase.Create(ControlTypes.Output);
            outputControl.SourceObject = this.Section;
            outputControl.Settings.UpdateDynamically = true;

            this.SectionOutput.Children.Clear();
            this.SectionOutput.Children.Add(outputControl);

            sectionControl.InputChanged += new InputControl.InputEventHandler(outputControl.Refresh);


            InputControl designCont = (InputControl)ControlBase.Create(ControlTypes.Input);
            designCont.Settings.UpdateDynamically = true;
            designCont.SourceObject = this.Design;
            this.DesignInput.Children.Clear();
            this.DesignInput.Children.Add(designCont);
            designCont.InputChanged += Check;

        }

        void Check()
        {
            if (Force != null && Section != null)
            {
                Section.CalculateSecProps();
                Design.Result.Clear();
                Design.CheckASD(Force);
                if (Design.Result == true)
                    this.ResultSuccess = true;
                else
                    this.ResultSuccess = false;
            }
        }

        private void btnQuickReport_Click(object sender, RoutedEventArgs e)
        {
            if (Design == null)
                return;

            string s = "";
            foreach (StructuralBase.Design.DesignMessage message in Design.Result.Messages)
            {
                s += message.Message + "\n";
            }
            if (s == "")
                s += "Herhangi bir mesaj yok";
            if (Design.Result == false)
                MainWindow.extendedNotifyIcon.targetNotifyIcon.ShowBalloonTip(1000, "Advices", s, System.Windows.Forms.ToolTipIcon.Warning);
            else
                MainWindow.extendedNotifyIcon.targetNotifyIcon.ShowBalloonTip(1000, "Problems", s, System.Windows.Forms.ToolTipIcon.Error);
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            LaTeX.LateX newLat = new LaTeX.LateX();
            newLat.StartDocument();

            newLat.AddHeading("Yükleme", LaTeX.HeadingStyles.H3);

            LaTeX.Tabular loading = new LaTeX.Tabular();
            loading.DenseOfVertical(this.Force, true);
            newLat.Add(loading);

            newLat.AddHeading("Kesit Boyutları", LaTeX.HeadingStyles.H3);

            LaTeX.Tabular section = new LaTeX.Tabular();
            section.DenseOfVertical(this.Section, true);
            newLat.Add(section);

            newLat.AddHeading("Kesit Özellikleri", LaTeX.HeadingStyles.H3);

            LaTeX.Tabular sectionProps = new LaTeX.Tabular();
            sectionProps.Columns.Add("A").CellType = LaTeX.CellTypes.Double;
            sectionProps.Columns["A"].Header = "Alan {[}$cm^2${]}";
            sectionProps.Columns["A"].Floating = 2;
            sectionProps.Columns.Add("I33").CellType = LaTeX.CellTypes.Double;
            sectionProps.Columns["I33"].Header = "I33 {[}$cm^4${]}";
            sectionProps.Columns["I33"].Floating = 2;
            sectionProps.Columns.Add("I22").CellType = LaTeX.CellTypes.Double;
            sectionProps.Columns["I22"].Header = "I22 {[}$cm^4${]}";
            sectionProps.Columns["I22"].Floating = 2;
            sectionProps.Columns.Add("W33").CellType = LaTeX.CellTypes.Double;
            sectionProps.Columns["W33"].Header = "W33 {[}$cm^3${]}";
            sectionProps.Columns["W33"].Floating = 2;
            sectionProps.Columns.Add("W22").CellType = LaTeX.CellTypes.Double;
            sectionProps.Columns["W22"].Header = "W22 {[}$cm^3${]}";
            sectionProps.Columns["W22"].Floating = 2;
            sectionProps.Rows.Add(Section.A, Section.I33, Section.I22, Section.W33, Section.W22);
            newLat.Add(sectionProps);

            newLat.AddHeading("Kesit Allowable Strengths(ASD)", LaTeX.HeadingStyles.H3);
            LaTeX.Tabular asdStrengths = new LaTeX.Tabular();
            asdStrengths.HasHeader = false;
            asdStrengths.AddColumns(2);
            asdStrengths.Columns.ColumnStyle = new LaTeX.Style.ColumnStyle { Alignment = LaTeX.Style.HorizontalAlignment.Left };
            asdStrengths.Rows.Add("Güçlü Eksen Moment", Design.Mc);
            asdStrengths.Rows.Add("Kesme", Design.Vc);
            asdStrengths.Rows.Add("Çekme", Design.Ptc);
            asdStrengths.Rows.Add("Basınç", Design.Pc);
            asdStrengths.Rows.Add("Zayıf Eksen Moment", Design.Mcy);
            newLat.Add(asdStrengths);

            newLat.EndDocument();
            newLat.Execute("sonuc", true);
            
        }

        
    }

    public static class Ext
    {
        public static string[] GetAttrs(this Rectangular me)
        {
            return new string[] { "H", "B", "Tw", "Tf" };
        }
    }
}
