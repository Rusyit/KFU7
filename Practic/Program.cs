using System;
using System.Collections.Generic;

namespace Practic
{
    enum Worklvls
    {
        системщики,
        разработчики,
        начальство,
    }

    class Employee
    {
        private string employeeName;
        private string job;
        private Worklvls worklvls;
        private List<Employee> boss = new List<Employee>();

        public string EmployeeName
        {
            get
            {
                return employeeName;
            }
        }

        public string Job
        {
            get
            {
                return job;
            }
        }
        public Worklvls WorkDepartment
        {
            get
            {
                return worklvls;
            }
        }

        public List<Employee> Boss
        {
            get
            {
                return boss;
            }
        }

        public void doTask(Task task, Employee employee)
        {
            Console.WriteLine($"От {employee.EmployeeName} ({employee.Job}) дается задача\n{task.TaskCon} работнику {employeeName} ({job})");

            if (boss.Contains(employee) && (worklvls == task.TaskAdd))
            {
                Console.WriteLine($"{employeeName} ({job}) берет задачу\n");
            }
            else
            {
                Console.WriteLine($"{employeeName} ({job}) не берет задачу\n");
            }
        }

        public Employee(string employeeName, string job, Worklvls worklvls, params Employee[] boss)
        {
            this.employeeName = employeeName;
            this.job = job;
            this.worklvls = worklvls;
            this.boss.AddRange(boss);
        }
    }

    class Task
    {
        private string taskCon;
        private Worklvls taskAdd;

        public string TaskCon
        {
            get
            {
                return taskCon;
            }
        }

        public Worklvls TaskAdd
        {
            get
            {
                return taskAdd;
            }
        }

        public Task(string taskCon, Worklvls taskAdd)
        {
            this.taskCon = taskCon;
            this.taskAdd = taskAdd;
        }
    }


    class Program
    {
        static void Main()
        {
            // ПРАКТИЧЕСКОЕ ДОМАШНЕЕ ЗАДАНИЕ
            Console.WriteLine("ПРАКТИЧЕСКОЕ ДОМАШНЕЕ ЗАДАНИЕ\n");

            Employee Semyon = new Employee("Семен", "Генеральный директор", Worklvls.начальство);

            Employee Rashid = new Employee("Рашид", "Финансовый директор", Worklvls.начальство, Semyon);
            Employee Lucas = new Employee("Лукас", "Начальник бухгалтерии", Worklvls.начальство, Rashid);

            Employee OIlham = new Employee("О Ильхам", "Директор по автоматизации", Worklvls.начальство, Semyon);
            Employee Orkady = new Employee("Оркадий", "Начальник отдела информационных технологий", Worklvls.начальство, OIlham);
            Employee Volodya = new Employee("Володя", "Зам. начальника отдела информационных технологий", Worklvls.начальство, Orkady);

            Employee Ilshat = new Employee("Ильшат", "Начальник отдела системщиков", Worklvls.начальство, Orkady, Volodya);
            Employee Ivanych = new Employee("Иваныч", "Зам. начальника отдела системщиков", Worklvls.начальство, Ilshat);

            Employee Ilya = new Employee("Илья", "Работник отдела системщиков", Worklvls.системщики, Ivanych);
            Employee Vitya = new Employee("Витя", "Работник отдела системщиков", Worklvls.системщики, Ivanych);
            Employee Zhenya = new Employee("Женя", "Работник отдела системщиков", Worklvls.системщики, Ivanych);

            Employee Sergey = new Employee("Сергей", "Начальник отдела разработчиков", Worklvls.начальство, Orkady, Volodya);
            Employee Laysan = new Employee("Ляйсан", "Зам. начальника отдела разработчиков", Worklvls.начальство, Sergey);

            Employee Marat = new Employee("Марат", "Работник отдела разработчиков", Worklvls.разработчики, Laysan);
            Employee Dina = new Employee("Дина", "Работник отдела разработчиков", Worklvls.разработчики, Laysan);
            Employee Ildar = new Employee("Ильдар", "Работник отдела разработчиков", Worklvls.разработчики, Laysan);
            Employee Anton = new Employee("Антон", "Работник отдела разработчиков", Worklvls.разработчики, Laysan);

            Task task1 = new Task("Расчитать прибыль компании", Worklvls.начальство);
            Task task2 = new Task("Разработать приложение", Worklvls.разработчики);
            Task task3 = new Task("Настроить работу корпоративной сети", Worklvls.системщики);
            Task task4 = new Task("Провести собрание с сотрудниками", Worklvls.начальство);

            Ilya.doTask(task3, Ivanych);
            Vitya.doTask(task3, Lucas);
            Zhenya.doTask(task1, Ivanych);

            Marat.doTask(task2, Laysan);
            Dina.doTask(task3, Laysan);
            Ildar.doTask(task2, Sergey);
            Anton.doTask(task4, Marat);

            Sergey.doTask(task4, Orkady);
            Sergey.doTask(task4, Volodya);
        }
    }
}
