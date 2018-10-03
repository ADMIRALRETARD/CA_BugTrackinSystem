using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_BugTrackerSystemC
{
    class Program
    {
        static void Main(string[] args)
        {
            	//TODO:
		//1)Сделать меню
		//2)Исключения
		//3)Декомпозировать методы
            Console.ReadLine();

        }


        #region Queries

        static void ShowTaskInProject()
        {
            using(var db =new BugTrackerEntities())
            {
                Console.WriteLine("Введите название проекта :");
                string projectName = Console.ReadLine();
                var tasks = db.Tasks.Where(p => p.Project.ProjectName == projectName).AsEnumerable();
                foreach(var item in tasks)
                {
                    Console.WriteLine("Task Name  : "+item.Theme);
                }
                

            }

        }

        static void ShowTaskForUser()
        {
            using(var db=new BugTrackerEntities())
            {
                Console.WriteLine("Введите имя пользователя");
                string username = Console.ReadLine();
                var tasks = db.Tasks.Where(u => u.User.FirstName == username || u.User.LastName == username).AsEnumerable();
                Console.WriteLine("Tasks for user : {0}",username);
                foreach (var item in tasks)
                {
                    Console.WriteLine("Task Name :"+item.Theme);
                }

            }
        }

        #endregion

        #region Show
        static void ShowTask()
        {
            using (var db = new BugTrackerEntities())
            {
                var tasks = db.Tasks.ToList();
                Console.WriteLine("Id " + "\tНазвание" + "\tProjectName" +
                        "\tUserName " + " \tType " + "\tPriority" + "\tDescription");
                foreach (var item in tasks)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}   \t{4}\t{5}     \t{6}", item.Id, item.Theme, item.Project.ProjectName,
                        item.User.LastName, item.Type, item.Priority, item.Description);
                }
            }
        }

        static void ShowUsers()
        {
            using (var db = new BugTrackerEntities())
            {
                var users = db.Users.ToList();
                Console.WriteLine("Id" + "\tFirstName" + "\tLastName");
                foreach (var item in users)
                {
                    Console.WriteLine("{0}\t{1}   \t{2}", item.Id, item.FirstName, item.LastName);
                }
            }
        }
        static void ShowProjects()
        {
            using (var db = new BugTrackerEntities())
            {
                var projects = db.Projects.ToList();
                Console.WriteLine("Id" + "\tProjectName");
                foreach (var item in projects)
                {
                    Console.WriteLine("{0}  \t{1}", item.Id, item.ProjectName);
                }
            }
        }
        #endregion

        #region IdFinder
        static int FindProjectId(string _projectName)
        {
            string projectName = _projectName;
            using (var db = new BugTrackerEntities())
            {
                Project project = db.Projects.Where(c => c.ProjectName == projectName).
                        FirstOrDefault();
                return project.Id;
            }
        }
        static int FindUserId(string name)
        {
            string username = name;
            using (var db = new BugTrackerEntities())
            {
                User user = db.Users.Where(u => u.LastName == username || u.FirstName == username).
                    FirstOrDefault();
                return user.Id;
            }
        }
        #endregion

        #region Add
        static void AddUser()
        {
            Console.WriteLine("Введите Имя пользователя");
            string firstname = Console.ReadLine().ToString();
            while (firstname == "")
            {
                Console.WriteLine("Вы не ввели имя");
                firstname = Console.ReadLine().ToString();

            }
            Console.WriteLine("Введите Фамилию пользователя");
            string lastname = Console.ReadLine().ToString();
            using (var db = new BugTrackerEntities())
            {
                User newUser = new User()
                {
                    FirstName = firstname,
                    LastName = lastname
                };

                db.Users.Add(newUser);
                db.SaveChanges();
                Console.WriteLine("Пользователь добавлен");
            }


        }
        static void AddProject()
        {
            Console.WriteLine("Введите название проекта");
            string projectName = Console.ReadLine().ToString();
            using (var db = new BugTrackerEntities())
            {
                Project newProject = new Project()
                {
                    ProjectName = projectName
                };

                db.Projects.Add(newProject);
                db.SaveChanges();
                Console.WriteLine("Проект добавлен");
            }
        }
        static void AddTask()
        {
            Console.WriteLine("Введите название задачи:");
            string theme = Console.ReadLine().ToString();
            Console.WriteLine("Введите название проекта:");
            string projectName = Console.ReadLine().ToString();
            Console.WriteLine("Введите фамилию исполнителя :");
            string userName = Console.ReadLine().ToString();
            Console.WriteLine("Введите тип задачи :");
            string tasktype = Console.ReadLine().ToString();
            Console.WriteLine("Введите приоритет задачи");
            string priority = Console.ReadLine().ToString();
            Console.WriteLine("Введите описание для задачи :");
            string description = Console.ReadLine().ToString();
            using (var db = new BugTrackerEntities())
            {


                Task newtask = new Task()
                {
                    Theme = theme,
                    ProjectID = FindProjectId(projectName),
                    UserID = FindUserId(userName),
                    Type = tasktype,
                    Priority = priority,
                    Description = description
                };
                db.Tasks.Add(newtask);
                db.SaveChanges();
            }
            Console.WriteLine("Задача добавлена");


        }
        #endregion

        #region Del
        static void DeleteUser()
        {
            Console.WriteLine("Введите Id пользователя, которого следует удалить :");
            var id = Convert.ToInt32(Console.ReadLine());

            using (var db = new BugTrackerEntities())
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();

            }
            Console.WriteLine("Пользователь удален");


        }
        static void DeleteProject()
        {
            Console.WriteLine("Введите Id проекта, который следует удалить :");
            var id = Convert.ToInt32(Console.ReadLine());

            using (var db = new BugTrackerEntities())
            {
                var project = db.Projects.Find(id);
                db.Projects.Remove(project);
                db.SaveChanges();

            }
            Console.WriteLine("Проект удален");
        }
        static void DeleteTask()
        {
            Console.WriteLine("Введите Id задачи , которую следует удалить :");
            var id = Convert.ToInt32(Console.ReadLine());

            using (var db = new BugTrackerEntities())
            {
                var task = db.Tasks.Find(id);
                db.Tasks.Remove(task);
                db.SaveChanges();

            }
            Console.WriteLine("Задача удалена");
        }


        #endregion

        #region Edit
        static void EditUser()
        {
            Console.WriteLine("Введите Id пользователя,которого следует изменить");
            var id = Convert.ToInt32(Console.ReadLine());

            using (var db = new BugTrackerEntities())
            {
                var user = db.Users.Find(id);
                if (user == null) return;

                string newFirstName = Console.ReadLine().ToString();
                string newLastName = Console.ReadLine().ToString();
                user.FirstName = newFirstName;
                user.LastName = newLastName;

                db.Users.AddOrUpdate(user);


                db.SaveChanges();

            }
            Console.WriteLine("Данные изменены");
        }

        static void EditProject()
        {
            Console.WriteLine("Введите Id проекта,который следует изменить");
            var id = Convert.ToInt32(Console.ReadLine());

            using (var db = new BugTrackerEntities())
            {
                var project = db.Projects.Find(id);
                if (project == null) return;

                string newProjectName = Console.ReadLine().ToString();

                project.ProjectName = newProjectName;

                db.Projects.AddOrUpdate(project);
                db.SaveChanges();

            }
            Console.WriteLine("Данные изменены");

        }
        static void EditTask()
        {
            Console.WriteLine("Введите Id задачи,которую следует изменить");
            var id = Convert.ToInt32(Console.ReadLine());

            using (var db = new BugTrackerEntities())
            {
                var task = db.Tasks.Find(id);
                if (task == null) return;
                Console.WriteLine("new theme");
                string newTheme = Console.ReadLine().ToString();
                Console.WriteLine("new user");
                string newUser = Console.ReadLine().ToString();
                Console.WriteLine("new Project");
                string newProject = Console.ReadLine().ToString();
                Console.WriteLine("new Type");
                string newType = Console.ReadLine().ToString();
                Console.WriteLine("new priority");
                string newPriority = Console.ReadLine().ToString();
                Console.WriteLine("new Description");
                string newDescription = Console.ReadLine().ToString();

                task.Theme = newTheme;
                task.UserID = FindUserId(newUser);
                task.ProjectID = FindProjectId(newProject);
                task.Type = newType;
                task.Priority = newPriority;
                task.Description = newDescription;

                db.Tasks.AddOrUpdate(task);
                db.SaveChanges();

            }
            Console.WriteLine("Данные изменены");
        }


        #endregion
    }
}
