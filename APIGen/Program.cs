using Scriban;
using System.Runtime.InteropServices;
using System.Text;
using Wrapper;

namespace APIGen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var template = Template.Parse(@"
using System.Numerics;

unsafe public class BepuNative
{
    static nint handle => NativeLibrary.Load(""Wrapper.dll"");
    {{content}}
}");

            var fieldTemplate = Template.Parse(@"public static delegate* unmanaged<{{types}}> {{name}} = (delegate* unmanaged<{{types}}>)NativeLibrary.GetExport(handle, nameof({{name}}));");
            var content = new StringBuilder();
            var methods = typeof(BepuWrapper).GetMethods();
            foreach (var method in methods)
                if (Attribute.IsDefined(method, typeof(UnmanagedCallersOnlyAttribute)))
                {
                    Console.WriteLine(method.ReturnType.Name);
                    var returnType = method.ReturnType.Name;
                    if (returnType== typeof(void).Name)
                        returnType = "void";
                    content.AppendLine(fieldTemplate.Render(new { name = method.Name, types = returnType }));
                }

            var output = template.Render(new { content });

            Console.WriteLine(output);
        }
    }
}
