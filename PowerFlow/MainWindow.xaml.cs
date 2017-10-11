using System;
using System.Activities;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;
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

namespace PowerFlow
{
    public static class Extensions
    {
        public static ToolboxItemWrapper GetItemWrapper(this Type t, string name)
        {
            ToolboxItemWrapper tool3 =
                new ToolboxItemWrapper(t.FullName, t.Assembly.FullName, null, name);
            return tool3;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WorkflowDesigner Designer { get; set; }
        public Sequence _blank_flow;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.MouseDown += MainWindow_MouseDown;

            btn_close.Click += btn_close_Click;
            btn_maximize.Click += btn_maximize_Click;
            btn_minimize.Click += btn_minimize_Click;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDesigner();
            _blank_flow = new Sequence();
            (new DesignerMetadata()).Register();
            Designer.Load(_blank_flow);
        }

        private void LoadDesigner()
        {
            Designer = new WorkflowDesigner();

            var tbx = GetToolboxControl();
            grid_designsurface.Children.Add(tbx);
            grid_designsurface.Children.Add(Designer.View);
            grid_designsurface.Children.Add(Designer.PropertyInspectorView);

            Grid.SetColumn(tbx, 0);
            Grid.SetColumn(Designer.View, 1);
            Grid.SetColumn(Designer.PropertyInspectorView, 2);
            

            /**
             * Load activity designers
             * 
             **/
            (new DesignerMetadata()).Register();
            AttributeTableBuilder builder = new AttributeTableBuilder();

            //Register Custom Designers.
            builder.AddCustomAttributes(typeof(CADesigner), new DesignerAttribute(typeof(CADesigner)));
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }

        private ToolboxControl GetToolboxControl()
        {
            //Create the ToolBoxControl
            ToolboxControl ctrl = new ToolboxControl();

            ToolboxCategory category_control = new ToolboxCategory("Control");
            category_control.Add(typeof(If).GetItemWrapper("If"));
            category_control.Add(typeof(Switch<bool>).GetItemWrapper("Switch"));
            category_control.Add(typeof(While).GetItemWrapper("While"));
            category_control.Add(typeof(Sequence).GetItemWrapper("Sequence"));


            //Adding the toolboxItems to the category.
            ToolboxCategory category_primitives = new ToolboxCategory("Primitives");
            category_primitives.Add(typeof(TryCatch).GetItemWrapper("Try Catch"));
            category_primitives.Add(typeof(Throw).GetItemWrapper("Throw"));
            category_primitives.Add(typeof(Assign).GetItemWrapper("Assign"));

            //custom
            ToolboxCategory category_custom = new ToolboxCategory("Custom");
            category_custom.Add(typeof(CompositeActivity).GetItemWrapper("Composite Activity"));
            category_custom.Add(typeof(Test).GetItemWrapper("test"));


            ////Create a collection of category items
            //ToolboxCategory category_items = new ToolboxCategory("Activities");
            ////example of loading a dynamic assembly not statically bound to the host executable
            //var assembly = Assembly.LoadFile(System.IO.Path.Combine(Environment.CurrentDirectory, "codeanalysislibrary.dll"));
            //var activities = assembly.GetTypes().Where(i => i.BaseType.FullName.ToLower() == "system.activities.codeactivity");
            //foreach (var activity in activities)
            //{
            //    var dynamically_loaded_type = assembly.GetType(activity.FullName);
            //    if (dynamically_loaded_type != null)
            //    {
            //        var toolbox_info = dynamically_loaded_type.GetCustomAttribute<ToolboxInfoAttribute>();
            //        category_items.Add(dynamically_loaded_type.GetItemWrapper(toolbox_info.Name));
            //    }
            //}


            //Adding the category to the ToolBox control.
            ctrl.Categories.Add(category_control);
            ctrl.Categories.Add(category_primitives);
            ctrl.Categories.Add(category_custom);
            return ctrl;
        }

        void btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        void btn_maximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != System.Windows.WindowState.Maximized)
                this.WindowState = System.Windows.WindowState.Maximized;
            else
                this.WindowState = System.Windows.WindowState.Normal;
        }

        void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Designer.Save("../../saved_workflow.xml");
            WorkflowInvoker.Invoke(_blank_flow);
        }
    }
}
