using Scriban;
using System.Runtime.InteropServices;
using System.Text;
using Wrapper;

namespace APIGen;

internal class Program
{
    static void Main(string[] args)
    {
        var outputPath = $"../../../../AOTTest/BepuNative.cs";
        var output = GetProcAddressFunctionPointer();
        File.WriteAllText(outputPath, output);
        Console.WriteLine(output);
        Console.WriteLine("Output to " + Path.GetFullPath(outputPath));
    }

    static string GetProcAddressFunctionPointer()
    {
        var templateString = File.ReadAllText("../../../GetProcAddressTemplate.txt");
        var template = Template.Parse(templateString);
        var declarationTemplate = Template.Parse(@"public static delegate* unmanaged[Stdcall]<{{types}}> {{name}};");
        var initTemplate = Template.Parse(@"{{name}} = (delegate* unmanaged[Stdcall]<{{types}}>)GetProcAddress(library, nameof({{name}}));");
        var declaration = new StringBuilder();
        var init = new StringBuilder();
        var methods = typeof(BepuWrapper).GetMethods();
        foreach (var method in methods)
            if (Attribute.IsDefined(method, typeof(UnmanagedCallersOnlyAttribute)))
            {
                var types = new StringBuilder();
                foreach (var param in method.GetParameters())
                {
                    types.Append(GetShortType(param.ParameterType));
                    types.Append(",");
                }

                types.Append(GetShortType(method.ReturnType));
                declaration.AppendLine(declarationTemplate.Render(new { name = method.Name, types = types }));
                init.AppendLine(initTemplate.Render(new { name = method.Name, types = types }));
            }

        return template.Render(new { declaration, init });
    }

    static string CompactFunctionPointer()
    {
        var templateString = File.ReadAllText("../../../template.txt");
        var template = Template.Parse(templateString);
        var fieldTemplate = Template.Parse(@"public static delegate* unmanaged<{{types}}> {{name}} = (delegate* unmanaged<{{types}}>)NativeLibrary.GetExport(handle, nameof({{name}}));");
        var content = new StringBuilder();
        var methods = typeof(BepuWrapper).GetMethods();
        foreach (var method in methods)
            if (Attribute.IsDefined(method, typeof(UnmanagedCallersOnlyAttribute)))
            {
                var types = new StringBuilder();
                foreach (var param in method.GetParameters())
                {
                    types.Append(GetShortType(param.ParameterType));
                    types.Append(",");
                }

                types.Append(GetShortType(method.ReturnType));
                content.AppendLine(fieldTemplate.Render(new { name = method.Name, types = types }));
            }

        return template.Render(new { content });
    }


    static string GetShortType(Type type)
    {
        switch (type.Name)
        {
            case nameof(Byte):
                return "byte";
            case nameof(SByte):
                return "sbyte";
            case nameof(Int16):
                return "short";
            case nameof(Int32):
                return "int";
            case nameof(Int64):
                return "long";
            case nameof(UInt16):
                return "ushort";
            case nameof(UInt32):
                return "uint";
            case nameof(UInt64):
                return "ulong";
            case nameof(Single):
                return "float";
            case nameof(Double):
                return "double";
            case nameof(Decimal):
                return "decimal";
            case nameof(Boolean):
                return "bool";
            case nameof(Char):
                return "char";
            case nameof(String):
                return "string";
            case nameof(Object):
                return "object";
            case "Void":
                return "void";
            default:
                return type.Name;
        }
    }
}
