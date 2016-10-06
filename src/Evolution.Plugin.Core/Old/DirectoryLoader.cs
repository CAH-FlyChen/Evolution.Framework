//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.Loader;
//using System.Threading.Tasks;

//namespace Evolution.Plugin.Core
//{
//    public class DirectoryLoader
//    {
//        private readonly AssemblyLoadContext _context;
//        private readonly DirectoryInfo _path;

//        public DirectoryLoader(DirectoryInfo path, AssemblyLoadContext context)
//        {
//            _path = path;
//            _context = context;
//        }

//        public Assembly Load(AssemblyName assemblyName)
//        {
//            return _context.LoadFromAssemblyPath(Path.Combine(_path.FullName, assemblyName.Name + ".dll"));
//        }

//        public IntPtr LoadUnmanagedLibrary(string name)
//        {
//            //this isn't going to load any unmanaged libraries, just throw
//            throw new NotImplementedException();
//        }
//    }
//}
