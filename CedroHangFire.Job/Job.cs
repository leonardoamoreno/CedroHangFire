using Microsoft.EntityFrameworkCore;
using System;

namespace CedroHangFire.Job
{
    public class JobExecute: DbContext
    {
        public void OnExecute() {
            
            Console.Write("teste ok");
        }
    }
}
