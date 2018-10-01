using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_BugTrackerSystemC
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddUser();
            // AddProject();
            //AddTask();
            //DeleteUser();
            using (var db = new BugTrackerEntities())
            {
                var tasks = db.Tasks.ToList();
                Console.WriteLine("Id "+ "\tНазвание"+ "\tProjectName" +
                        "\tUserName " + "\tType "+"\tPriority"+"\tDescription");
                foreach (var item in tasks)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}    \t{4}\t{5}     \t{6}",item.Id,item.Theme,item.Project.ProjectName,
                        item.User.LastName,item.Type,item.Priority,item.Description);
                }

                //var users = db.Users.ToList();
                //Console.WriteLine("Id" + "\tFirstName" + "\tLastName");
                //foreach (var item in users)
                //{
                //    Console.WriteLine(item.Id + "\t" + item.FirstName + "\t" + item.LastName);
                //}




                //var projects = db.Projects.ToList();
                //Console.WriteLine("Id" + "\tProjectName");
                //foreach (var item in projects)
                //{
                //    Console.WriteLine(item.Id + "\t" + item.ProjectName);
                //}



            }
            
                Console.ReadLine();

        }

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
                User user = db.Users.Where(u => u.LastName == username||u.FirstName==username).
                    FirstOrDefault();
                return user.Id;
            }
        }
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
            string tasktype= Console.ReadLine().ToString();
            Console.WriteLine("Введите приоритет задачи");
            string priority= Console.ReadLine().ToString();
            Console.WriteLine("Введите описание для задачи :");
            string description= Console.ReadLine().ToString();
            using(var db=new BugTrackerEntities())
            {


                Task newtask = new Task()
                {
                    Theme = theme,
                    ProjectID = FindProjectId(projectName),
                    UserID = FindUserId(userName),
                    Type=tasktype,
                    Priority=priority,
                    Description=description
                };
                db.Tasks.Add(newtask);
                db.SaveChanges();
            }
            Console.WriteLine("Задача добавлена");


        }
    }
}
