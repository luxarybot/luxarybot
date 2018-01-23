//using Luxary.Services;
//using LiteDB;

//namespace Luxary.Services
//{
//    class Database
//    {
//        private static Database instance;
//        private UserDao UserDao;

//        private Database()
//        {
//            LiteDatabase liteDatabase = new LiteDatabase("database.db");

//            UserDao = new UserDao(liteDatabase, "Users");
//        }

//        public static Database GetInstance()
//        {
//            if (instance == null)
//            {
//                instance = new Database();
//            }
//            return instance;
//        }

//        public UserDao GetUserDao()
//        {
//            return UserDao;
//        }
//    }

//    public interface IEntity
//    {
//        int Id { get; set; }
//    }
//}