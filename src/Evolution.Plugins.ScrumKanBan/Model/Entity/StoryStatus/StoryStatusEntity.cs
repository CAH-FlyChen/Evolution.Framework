using Evolution.Data;
using Evolution.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Evolution.Plugins.ScrumKanBan.Models
{
    [Table("DM_StoryStatus")]
    public class StoryStatusEntity: EntityBase
    {
        [Key]
        public string Code { get; set; }
        public string Text { get; set; }

        public string ButtonDisplayName { get; set; }
    }





    public class StoryStatusList
    {
        private static List<StoryStatusEntity> status = new List<StoryStatusEntity>();
        public static List<StoryStatusEntity> GetStatusList(KanBanDbContext ctx)
        {
            if (!status.Any())
            {
                    status = ctx.StoryStatus.OrderBy(t=>t.SortCode).ToList();
            }
            return status;
        }

        public static string GetStatusText(string code, KanBanDbContext ctx)
        {
            GetStatusList(ctx);
            var r = status.SingleOrDefault(x => x.Code==code);
            if(default(KeyValuePair<string, string>).Equals(r))
            {
                return ""; 
            }
            else
            {
                return r.Text;
            }
        }

        public static StoryStatusEntity GetNextStatus(string statusCode, KanBanDbContext ctx)
        {
            GetStatusList(ctx);
            for(int i=0;i< status.Count;i++)
            {
                if(status[i].Code==statusCode)
                {
                    if ((i + 1 )<= status.Count)
                        return status[i + 1];
                    else
                        return status[0];
                }
            }
            return null;
        }
        public static StoryStatusEntity GetNextStatusButtonDisplay(string statusCode, KanBanDbContext ctx)
        {
            GetStatusList(ctx);
            for (int i = 0; i < status.Count; i++)
            {
                if (status[i].Code == statusCode)
                {
                    if ((i + 1) <= status.Count)
                        return status[i + 1];
                    else
                        return status[0];
                }
            }
            return null;
        }
        public static StoryStatusEntity GetStatusButtonDisplay(string statusCode, KanBanDbContext ctx)
        {
            GetStatusList(ctx);

            for (int i = 0; i < status.Count; i++)
            {
                if (status[i].Code == statusCode)
                {
                        return status[i];
                }
            }
            return null;
        }
    }
}
