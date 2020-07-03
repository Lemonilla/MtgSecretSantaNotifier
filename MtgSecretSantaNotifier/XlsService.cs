using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgSecretSantaNotifier
{
    class XlsService
    {
        private readonly string idAttrName = ConfigurationManager.AppSettings["AttributeNameForId"];
        private static XlsService _instance = null;
        public static XlsService instance { 
            get {
                if (_instance == null) _instance = new XlsService();
                return _instance;
            } 
        }
        private XlsService() {
            _columnNames = new List<Attribute>();
            _users = new List<User>();
        }

        // Columns
        private List<Attribute> _columnNames;
        public List<Attribute> Columns { get { return _columnNames; } }
        public void AddColumn(string columnName, int index)
        {
            _columnNames.Add(new Attribute(columnName, index));
        }

        // Rows (users)
        private List<User> _users;
        public List<User> Users { get { return _users; } }
        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public User GetUserById(string id)
        {
            foreach(User user in Users)
            {
                if (user.GetAttributeValue(idAttrName) == id) return user;
            }
            return null;
        }
    }
}
