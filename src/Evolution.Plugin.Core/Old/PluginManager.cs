//using Evolution.Plugin.Core;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Reflection;
//using System.Web;
//using System.Web.Compilation;
//using System.Web.Hosting;
//using Microsoft.AspNetCore.Hosting.Internal;

////[assembly: PreApplicationStartMethod(typeof(PluginManager), "Initialize")]
//namespace Evolution.Plugin.Core
//{
//    public class PluginManager
//    {
//        //约定：插件目录
//        private static string PluginsPath = "";
//        //约定：作为media信任级别时web.config中probing元素配置的加载目录
//        private static string ShadowCopyPath = "";
//        private static DirectoryInfo _shadowCopyFolder;
//        //插件列表
//        public static List<Type> PluginTypes = new List<Type>();

//        public static void Initialize(string webRootPath)
//        {
//            // /Plubins
//            PluginsPath = webRootPath + Path.DirectorySeparatorChar.ToString() + "Plugins";
//            // /Plugins/bin
//            ShadowCopyPath = PluginsPath + Path.DirectorySeparatorChar.ToString() + "bin";

//            //查找插件
//            var pluginFolder = new DirectoryInfo(PluginsPath);
//            Directory.CreateDirectory(pluginFolder.FullName);
//            var files = Directory.GetFiles(pluginFolder.FullName, "*.dll", SearchOption.AllDirectories);
//            //复制插件到运行时目录
//            //var isFullTrust = IsFullTrustLevel();
//            //if (IsFullTrustLevel())
//            //{
//                _shadowCopyFolder = new DirectoryInfo(AppDomain.CurrentDomain.DynamicDirectory);
//            //}
//            //else
//            //{
//            //    _shadowCopyFolder = new DirectoryInfo(HostingEnvironment.MapPath(ShadowCopyPath));
//            //    Directory.CreateDirectory(_shadowCopyFolder.FullName);
//            //    foreach (var item in _shadowCopyFolder.GetFiles())
//            //    {
//            //        File.Delete(item.FullName);
//            //    }
//            //}
//            foreach (var item in files)
//            {
//                var plug = new FileInfo(item);
//                var shadowCopiedPlug = new FileInfo(Path.Combine(_shadowCopyFolder.FullName, plug.Name));
//                //if (isFullTrust)
//                //{
//                    CopyFullTrust(plug, shadowCopiedPlug);
//                //}
//                //else
//                //{
//                //    CopyMediaTrust(plug, shadowCopiedPlug);
//                //}
//                //从运行时目录加载插件
//                var assembly = Assembly.Load(new AssemblyName(shadowCopiedPlug.FullName));
//                BuildManager.AddReferencedAssembly(assembly);
//                //添加插件类型到类型列表
//                foreach (var t in assembly.GetTypes())
//                {
//                    if (typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface && t.IsClass && !t.IsAbstract)
//                    {
//                        PluginTypes.Add(t);
//                        break;
//                    }
//                }
//            }
//        }

//        public static void ReStart()
//        {
//            if (IsFullTrustLevel())
//            {
//                HttpRuntime.UnloadAppDomain();
//                System.IO.File.SetLastWriteTimeUtc(HostingEnvironment.MapPath("~/global.asax"), DateTime.UtcNow);
//            }
//            else
//            {
//                System.IO.File.SetLastWriteTimeUtc(HostingEnvironment.MapPath("~/web.config"), DateTime.UtcNow);
//                System.IO.File.SetLastWriteTimeUtc(HostingEnvironment.MapPath("~/global.asax"), DateTime.UtcNow);
//            }
//        }

//        private static void CopyMediaTrust(FileInfo plug, FileInfo shadowCopiedPlug)
//        {
//            var shouldCopy = true;

//            if (shadowCopiedPlug.Exists)
//            {
//                var areFilesIdentical = shadowCopiedPlug.CreationTimeUtc.Ticks >= plug.CreationTimeUtc.Ticks;
//                if (areFilesIdentical)
//                {
//                    Debug.WriteLine("Not copying; files appear identical: '{0}'", shadowCopiedPlug.Name);
//                    shouldCopy = false;
//                }
//                else
//                {
//                    //delete an existing file
//                    Debug.WriteLine("New plugin found; Deleting the old file: '{0}'", shadowCopiedPlug.Name);
//                    File.Delete(shadowCopiedPlug.FullName);
//                }
//            }
//            if (shouldCopy)
//            {
//                try
//                {
//                    File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
//                }
//                catch (IOException)
//                {
//                    Debug.WriteLine(shadowCopiedPlug.FullName + " is locked, attempting to rename");
//                    //this occurs when the files are locked,
//                    //for some reason devenv locks plugin files some times and for another crazy reason you are allowed to rename them
//                    //which releases the lock, so that it what we are doing here, once it's renamed, we can re-shadow copy
//                    try
//                    {
//                        var oldFile = shadowCopiedPlug.FullName + Guid.NewGuid().ToString("N") + ".old";
//                        File.Move(shadowCopiedPlug.FullName, oldFile);
//                    }
//                    catch (IOException exc)
//                    {
//                        throw new IOException(shadowCopiedPlug.FullName + " rename failed, cannot initialize plugin", exc);
//                    }
//                    //ok, we've made it this far, now retry the shadow copy
//                    File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
//                }
//            }
//        }

//        private static void CopyFullTrust(FileInfo plug, FileInfo shadowCopiedPlug)
//        {
//            try
//            {
//                File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
//            }
//            catch (IOException)
//            {
//                Debug.WriteLine(shadowCopiedPlug.FullName + " is locked, attempting to rename");
//                try
//                {
//                    var oldFile = shadowCopiedPlug.FullName + Guid.NewGuid().ToString("N") + ".old";
//                    File.Move(shadowCopiedPlug.FullName, oldFile);
//                }
//                catch (IOException exc)
//                {
//                    throw new IOException(shadowCopiedPlug.FullName + " rename failed, cannot initialize plugin", exc);
//                }
//                //ok, we've made it this far, now retry the shadow copy
//                File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
//            }
//        }

//        private static bool IsFullTrustLevel()
//        {
//            var _trustLevel = AspNetHostingPermissionLevel.None;

//            //determine maximum
//            foreach (AspNetHostingPermissionLevel trustLevel in new[] {
//                                AspNetHostingPermissionLevel.Unrestricted,
//                                AspNetHostingPermissionLevel.High,
//                                AspNetHostingPermissionLevel.Medium,
//                                AspNetHostingPermissionLevel.Low,
//                                AspNetHostingPermissionLevel.Minimal
//                            })
//            {
//                try
//                {
//                    new AspNetHostingPermission(trustLevel).Demand();
//                    _trustLevel = trustLevel;
//                    break; //we've set the highest permission we can
//                }
//                catch (System.Security.SecurityException)
//                {
//                    continue;
//                }
//            }

//            return _trustLevel == AspNetHostingPermissionLevel.Unrestricted;
//        }
//    }
//}