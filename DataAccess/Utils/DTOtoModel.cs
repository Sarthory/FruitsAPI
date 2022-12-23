﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataAccess.Utils
{
    public class DTOtoModel
    {
        public static TOut Convert<TOut, TIn>(TIn fromRecord) where TOut : new()
        {
            var toRecord = new TOut();
            PropertyInfo[] fromFields = null;
            PropertyInfo[] toFields = null;

            fromFields = typeof(TIn).GetProperties();
            toFields = typeof(TOut).GetProperties();

            foreach (var fromField in fromFields)
            {
                foreach (var toField in toFields)
                {
                    if (fromField.Name == toField.Name)
                    {
                        toField.SetValue(toRecord, fromField.GetValue(fromRecord, null), null);
                        break;
                    }
                }

            }
            return toRecord;
        }

        public static List<TOut> Convert<TOut, TIn>(List<TIn> fromRecordList) where TOut : new()
        {
            return fromRecordList.Count == 0 ? null : fromRecordList.Select(Convert<TOut, TIn>).ToList();
        }
    }
}
