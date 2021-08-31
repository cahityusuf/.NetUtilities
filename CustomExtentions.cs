﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jkip.Application.Utilities
{
    public static class CustomExtentions
    {
        public static string HataMesajiGetir(this Exception ex)
        {
            if (ex == null)
                return "";

            StringBuilder sb = new StringBuilder();
            StringBuilder sbtrace = new StringBuilder();
            while (ex != null)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    if (sb.Length > 0)
                        sb.Append(" ");

                    sb.Append(ex.Message);
                    if (sbtrace.Length > 0)
                        sbtrace.Append(" ");

                    sbtrace.Append(ex.StackTrace);
                }

                ex = ex.InnerException;
            }

            return sb.ToString() + sbtrace.ToString();

        }
    }
}
