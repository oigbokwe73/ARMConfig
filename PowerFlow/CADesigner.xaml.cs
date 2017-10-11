using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PowerFlow
{
    // Interaction logic for CADesigner.xaml
    public partial class CADesigner
    {
        public CADesigner()
        {
            InitializeComponent();
        }


    }

    [
        Designer(typeof(CADesigner)),
        Description("this is a test activity")
    ]
    public class CompositeActivity : NativeActivity
    {
        TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

        public Activity Body { get; set; }
        public string Name { get; set; } = "Item";

        public int Number { get; set; } = 0;
        async protected override void Execute(NativeActivityContext context)
        {
            int total = 0;
            foreach (var item in Enumerable.Range(0, Number))
            {
                var instance = await context.ScheduleActivityAsync(Body, tcs, new CompletionCallback(Callback));
                total += this.Get("count");
            }

            
        }

        public void Callback(NativeActivityContext ctx, ActivityInstance inst)
        {
            tcs.TrySetResult(10);
        }
    }

    public class Test : CodeActivity
    {
        public InArgument<int> Value { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var count = this.Get("count");
            count += Value.Get<int>(context);
            this.Set("count", count);
        }
    }

    public static class ActivityState
    {
        static Dictionary<string, int> _state = new Dictionary<string, int>();

        public static void Set(this Activity a, string name, int state)
        {
            _state[name] = state;
        }

        public static int Get(this Activity a, string name)
        {
            if (!_state.ContainsKey(name))
            {
                _state.Add(name, 0);
            }
            return _state[name];
        }

        async public static Task<int> ScheduleActivityAsync(this NativeActivityContext context, Activity activity, TaskCompletionSource<int> tcs, CompletionCallback callback)
        {
            var instance = context.ScheduleActivity(activity, callback);
            await tcs.Task;
            return 10;
        }
    }
}
