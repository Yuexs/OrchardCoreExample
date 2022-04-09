using System;

namespace AliYunSecurityTokenServiceExample.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>long</returns>
        public static long ToTimeStamp(this DateTime time)
        {
            var startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, 0), TimeZoneInfo.Utc, TimeZoneInfo.Local);
            var t = (time.Ticks - startTime.Ticks) / 10000; //除10000调整为13位   
            return t;
        }

        /// <summary>
        ///     时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timeStamp)
        {
            var dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, 0), TimeZoneInfo.Utc, TimeZoneInfo.Local);
            var lTime = long.Parse(timeStamp + "0000");
            var toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 修复无法再mdb使用的问题
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FixFormat(this DateTime date)
        {
            return FixFormat(date, TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// 修复无法再mdb使用的问题
        /// </summary>
        /// <param name="date"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        public static DateTime FixFormat(this DateTime date, TimeSpan span)
        {
            var ticks = date.Ticks / span.Ticks;
            return new DateTime(ticks * span.Ticks, date.Kind);
        }
    }
}
