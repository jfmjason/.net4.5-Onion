
namespace Sghis.Core.Entity.QmsClient.Model
{
    public class QmsClientMenuAccess
    {
        public int Id { get; set; }
        public int ParentMenuId { get; set; }
        public int SubMenuId { get; set; }

        public string ParentMenuName { get; set; }
        public string SubMenuName { get; set; }
        public string Url { get; set; }
    }
}
