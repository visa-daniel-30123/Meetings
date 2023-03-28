using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var calendar1 = new List<string[]> { new string[] { "9:00", "10:30" }, new string[] { "12:00", "13:00" }, new string[] { "16:00", "18:00" } };
            var calendar1RangeLimits = new List<string[]> { new string[] { "9:00", "20:00" } };
            var calendar1Meets = calendar1.Select(x => new { Start = DateTime.Parse(x[0]), End = DateTime.Parse(x[1]) }).ToList();
            var calendar1Ranges = calendar1RangeLimits.Select(x => new { Start = DateTime.Parse(x[0]), End = DateTime.Parse(x[1]) }).First();

            var calendar2 = new List<string[]> { new string[] { "10:00", "11:30" }, new string[] { "12:30", "14:30" }, new string[] { "14:30", "15:00" }, new string[] { "16:00", "17:00" } };
            var calendar2RangeLimits = new List<string[]> { new string[] { "10:00", "18:30" } };
            var calendar2Meets = calendar2.Select(x => new { Start = DateTime.Parse(x[0]), End = DateTime.Parse(x[1]) }).ToList();
            var calendar2Ranges = calendar2RangeLimits.Select(x => new { Start = DateTime.Parse(x[0]), End = DateTime.Parse(x[1]) }).First();

            int meetingDuration = 30;


            foreach (var x in GeneratePossibleMeetingIntervals(GenerateFreeIntervals(calendar1Meets, calendar1Ranges), GenerateFreeIntervals(calendar2Meets, calendar2Ranges), meetingDuration))
            {
                Console.WriteLine(x.Start + " - " + x.End);
            }
            Console.ReadKey();
        }
         static List<dynamic> GenerateFreeIntervals(IEnumerable<dynamic> calendar1Meets, dynamic calendar1RangeLimits)
        {
    var freeIntervals = new List<dynamic>();
    for(int i=0;i<calendar1Meets.Count();i++){
        if(i==0){
            if(calendar1Meets.ElementAt(i).Start >calendar1RangeLimits.Start){
                freeIntervals.Add(new { Start = calendar1RangeLimits.Start, End = calendar1Meets.ElementAt(i).Start});
            }
        }
        else{
            if(calendar1Meets.ElementAt(i).Start > calendar1Meets.ElementAt(i-1).End){
                freeIntervals.Add(new {Start = calendar1Meets.ElementAt(i-1).End, End = calendar1Meets.ElementAt(i).Start});
            }
        }
        if(i==calendar1Meets.Count()-1){
            if(calendar1Meets.ElementAt(i).End < calendar1RangeLimits.End){
                freeIntervals.Add(new {Start = calendar1Meets.ElementAt(i).End, End = calendar1RangeLimits.End});
            }
        }
    }
    return freeIntervals;
}

    static List<dynamic> GeneratePossibleMeetingIntervals(List<dynamic> freeIntervals1, List<dynamic> freeIntervals2, int meetingDuration)
        {
    var possibleMeetingIntervals = new List<dynamic>();
    foreach(var x in freeIntervals1){
        foreach(var y in freeIntervals2){
            if(x.Start < y.Start){
                if(x.End > y.Start){
                    if(x.End > y.End){
                        possibleMeetingIntervals.Add(new {Start = y.Start, End = y.End});
                    }
                    else{
                        possibleMeetingIntervals.Add(new {Start = y.Start, End = x.End});
                    }
                }
            }
            else{
                if(y.End > x.Start){
                    if(y.End > x.End){
                        possibleMeetingIntervals.Add(new {Start = x.Start, End = x.End});
                    }
                    else{
                        possibleMeetingIntervals.Add(new {Start = x.Start, End = y.End});
                    }
                }
            }
        }
    }
    return possibleMeetingIntervals;
}
    }
}
