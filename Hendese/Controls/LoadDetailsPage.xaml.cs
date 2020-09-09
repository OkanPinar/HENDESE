using StructuralBase.Section;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// Interaction logic for LoadDetailsPage.xaml
    /// </summary>
    public partial class LoadDetailsPage : Page
    {

        Models.BaseModel Model = null;
        ListSections listSections;

        public LoadDetailsPage(Models.BaseModel Model)
        {
            InitializeComponent();
            this.Model = Model;

            this.ModelInput.Children.Clear();
            this.ModelOutput.Children.Clear();
            this.SectionsDatagrid.Children.Clear();
            
            InputControl inputCont = (InputControl)ControlBase.Create(ControlTypes.Input);
            inputCont.SourceObject = Model;
            inputCont.Settings.UpdateDynamically = true;
            inputCont.InputChanged += new InputControl.InputEventHandler(Model.Calculate);
            this.ModelInput.Children.Add(inputCont);

            OutputControl outputCont = (OutputControl)ControlBase.Create(ControlTypes.Output);
            outputCont.SourceObject = Model;
            outputCont.Settings.UpdateDynamically = true;

            inputCont.Submitted += new InputControl.SubmittedHandler(outputCont.Refresh);
            this.ModelOutput.Children.Add(outputCont);
        }


        private void GetSections_Click(object sender, RoutedEventArgs e)
        {
            Button btnObj = sender as Button;
            ContextMenu menu = btnObj.ContextMenu;

            List<SectionBase> sections = new List<SectionBase>();

            foreach (MenuItem group in menu.Items)
            {
                if (group.IsChecked != true)
                    continue;

                string fileName = group.Header.ToString();
                TextReader reader = File.OpenText(@"../../SectionProps/" + fileName + ".txt");

                string line;
                int count = sections.Count;
                while ((line = reader.ReadLine()) != null)
                {
                    SectionBase section = SectionBase.Create(fileName);
                    if (section == null)
                        section = SectionBase.Create(SectionType.Rectangular);
                    section.FromString(line);

                    if (sections.Count >= count + 3)
                        break;

                    section.GroupName = fileName;

                    if (Model.CheckSection(section))
                        sections.Add(section);
                }
            }
            
            this.SectionsDatagrid.Children.Clear();
            listSections = new ListSections(sections, Model.parameters);
            this.SectionsDatagrid.Children.Add(listSections);
        }
        
        private void menuGetReport_Click(object sender, RoutedEventArgs e)
        {
            if (listSections.dataGrid.Items.Count > 0)
            {
                ItemCollection sections = listSections.dataGrid.Items;
                LaTeX.LateX newLat = new LaTeX.LateX();
                newLat.StartDocument();

                newLat.AddSection("Model");
                LaTeX.Tabular loads = new LaTeX.Tabular();
                loads.Columns.ColumnStyle.Alignment = LaTeX.Style.HorizontalAlignment.Left;
                loads.DenseOfVertical(Model, true);
                newLat.Add(loads);

                LaTeX.Figure modelFig = new LaTeX.Figure();
                modelFig.Graphic.Path = "C:/Users/ismailk/Documents/Visual Studio 2015/Projects/Hendese/Hendese/Icons";
                modelFig.Graphic.FileName = Model.GetType().Name.Split('.').Last() + ".png";
                modelFig.Caption = "Model Çizimi";
                newLat.Add(modelFig);

                newLat.AddSection("Sections");
                LaTeX.Tabular newTabular = new LaTeX.Tabular();
                newTabular.Columns.Add("Name");
                newTabular.Columns.Add("A").CellType = LaTeX.CellTypes.Double;
                newTabular.Columns["A"].Floating = 2;
                newTabular.Columns.Add("I22").CellType = LaTeX.CellTypes.Double;
                newTabular.Columns["I22"].Floating = 2;
                newTabular.Columns.Add("I33").CellType = LaTeX.CellTypes.Double;
                newTabular.Columns["I33"].Floating = 2;
                newTabular.Columns.Add("W22").CellType = LaTeX.CellTypes.Double;
                newTabular.Columns["W22"].Floating = 2;
                newTabular.Columns.Add("W33").CellType = LaTeX.CellTypes.Double;
                newTabular.Columns["W33"].Floating = 2;
                newTabular.DenseOfObjects(sections);

                newTabular.Columns[0].Style.Alignment = LaTeX.Style.HorizontalAlignment.Left;

                newTabular.Columns.Add("IRatio");
                newTabular.Columns.Add("WRatio");
                int i = 0;
                foreach (SectionBase section in sections)
                {
                    newTabular[i, "IRatio"].Value = ((double)Model.parameters[0] / section.I33).ToString("0.00");
                    newTabular[i, "WRatio"].Value = ((double)Model.parameters[1] / section.W33).ToString("0.00");
                    i++;
                }

                //newTabular["IRatio"] = ((double)Model.parameters[0]) / newTabular["I33"];
                newLat.Add(newTabular);

                newLat.EndDocument();
                newLat.Execute("result", true);
            }
            else
            {
                MessageBox.Show("Uygun kesit bulunamadı.");
            }
        }
    }
}
