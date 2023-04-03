using Debugger;

namespace Testing {
    public static class Main {
        public static void Program() {
            Debugger.Console console = Debugger.Console.Instaciate;
            console.Log("Hello World");
            console.Dispose();
        }
    }
}
