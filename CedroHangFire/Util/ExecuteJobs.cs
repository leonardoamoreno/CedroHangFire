using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CedroHangFire.Util
{
    [DisplayName("{0}")]
    public class ExecuteJobs
    {
        [DisplayName("Job:{0}")]        
        public void Execute(string description, string PathFile) {
            LoadExecuteAssembly(PathFile);
        }

        private void LoadExecuteAssembly(string PathFile)
        {
            var asl = new AssemblyLoader(Path.GetDirectoryName(PathFile));
            var asm = asl.LoadFromAssemblyPath(PathFile);

            var type = asm.GetType("CedroHangfire.Job.JobExecute", true, true);
            dynamic obj = Activator.CreateInstance(type);
            obj.OnExecute();
        }
    }
}
