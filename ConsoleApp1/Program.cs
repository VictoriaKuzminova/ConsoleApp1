using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using static MyApp.Program;

namespace MyApp 
{
    internal class Program
    {
        public struct Student
        {
            public string FIO;
            public string Special;
            public string Group;
            public int Otmetka1;
            public int Otmetka2;
            public int Otmetka3;
            public Student(string fio, string special, string group, int ot1,int ot2, int ot3)
            {
                FIO = fio;
                Special = special;
                Group = group;
                Otmetka1 = ot1;
                Otmetka2 = ot2;
                Otmetka3 = ot3;
            }
        }
        static void WriteFile(string path,List<Student> students)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                foreach (Student student in students)
                {
                    writer.Write(student.FIO);
                    writer.Write(student.Special);
                    writer.Write(student.Group);
                    writer.Write(student.Otmetka1);
                    writer.Write(student.Otmetka2);
                    writer.Write(student.Otmetka3);
                }
                Console.WriteLine("Информация успешно записана в бинарный файл!");
            }

        }
        static List<Student> ReadFile(string path)
        {
            List<Student> result = new List<Student>();
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    string FIO = reader.ReadString();
                    string Special = reader.ReadString();
                    string Group = reader.ReadString();
                    int Otmetka1 = reader.ReadInt32();
                    int Otmetka2 = reader.ReadInt32();
                    int Otmetka3 = reader.ReadInt32();
                    result.Add(new Student(FIO, Special, Group,Otmetka1,Otmetka2,Otmetka3));
                }
            }
            return result;
        }
        static int InputCountStu()
        {
            int count=0;
            while(true)
            {
                try
                {
                    Console.Write("Введите кол-во студентов для обработки из диапазона от 1 до 20: ");
                    count = int.Parse(Console.ReadLine());
                    if ((count < 1) || (count > 20)) { throw new Exception();}
                    break;
                }
                catch { }
            }
            return count;
        }
        static List<Student> AddStudent(int count)
        {
            List<Student>students= new List<Student>();
            for(int i = 0; i < count; i++)
            {
                Console.WriteLine($"Ввод информации о СТУДЕНТЕ № {i + 1}");
                Console.Write("\t- введите фамилию: ");
                var fio = Console.ReadLine();
                Console.Write("\t- введите специальность: ");
                var special = Console.ReadLine();
                Console.Write("\t- введите группу: ");
                var group = Console.ReadLine();
                int otmetka1,otmetka2,otmetka3;
                while (true)
                {
                    try
                    {
                        Console.Write("\t- введите оценку №1: ");
                        otmetka1=int.Parse(Console.ReadLine());
                        Console.Write("\t- введите оценку №2: ");
                        otmetka2 = int.Parse(Console.ReadLine());
                        Console.Write("\t- введите оценку №3: ");
                        otmetka3 = int.Parse(Console.ReadLine());
                        if(((otmetka1>5)||(otmetka2>5) || (otmetka3>5)) || ((otmetka1 < 2) || (otmetka2 <2) || (otmetka3 < 2))) 
                        { throw new Exception();}
                        break;

                    }
                    catch{ }
                }
                students.Add(new Student {FIO=fio, Special=special,Group=group,Otmetka1=otmetka1,Otmetka2=otmetka2,Otmetka3=otmetka3});
            }
            return students;
        }
        static void PrintStudent(List<Student> students)
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("{0,10}{1,30}{2,30}{3,30}", "Фамилия", "Специальность", "Группу", "Оценки");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------");

            var orderStudent=from s in students
                             orderby s.FIO,s.Special
                             select s;
            foreach(var  student in orderStudent)
            {
                Console.WriteLine("{0,10}{1,30}{2,30}{3,30}", student.FIO, student.Special, student.Group, (student.Otmetka1+" " + student.Otmetka2+" " + student.Otmetka3));
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------");
        }    
        static List<Student> DeleteStudent(List<Student> students)
        {
            List<Student> result = new List<Student>();
            foreach (Student stu in students)
            {
                if(!((stu.Otmetka1== 5) && (stu.Otmetka2 == 5) && (stu.Otmetka3 == 5)))
                {
                    result.Add(new Student(stu.FIO, stu.Special, stu.Group, stu.Otmetka1, stu.Otmetka2, stu.Otmetka3));
                }
            }
            return result;
        }
        static List<Student> AddNewStudent(List<Student> students, string name)
        {
            List<Student>result= new List<Student>();
            List<Student> newStu=AddStudent(1);
            foreach(Student stu in students)
            {
                if (stu.FIO == name) 
                {
                    result.Add(new Student(stu.FIO, stu.Special, stu.Group, stu.Otmetka1, stu.Otmetka2, stu.Otmetka3));
                    foreach (Student stu2 in newStu)
                    result.Add(new Student(stu2.FIO, stu2.Special, stu2.Group, stu2.Otmetka1, stu2.Otmetka2, stu2.Otmetka3));
                }
                else
                result.Add(new Student(stu.FIO, stu.Special, stu.Group, stu.Otmetka1, stu.Otmetka2, stu.Otmetka3));
            }
            return result;

        }

        //чтение данных из файла
        static List<Student> ReadFileLine(string file)
        {
            List <Student> student= new List<Student>();
            using (StreamReader reader = new StreamReader(File.Open(file, FileMode.Open)))
            {
                while (reader.Peek() > -1)
                {
                    string FIO = reader.ReadLine();
                    string Special = reader.ReadLine();
                    string Group = reader.ReadLine();
                    int Otmetka1 = int.Parse(reader.ReadLine());
                    int Otmetka2 = int.Parse(reader.ReadLine());
                    int Otmetka3 = int.Parse(reader.ReadLine());
                    student.Add(new Student(FIO, Special, Group, Otmetka1, Otmetka2, Otmetka3));
                }
            }
            return student;
        }



       
        static void Main(string[] args)
        {
            //создание файла
            Console.Write("Введите имя бинарного файла для хранения данных о студентах: ");
            string nameFile = Console.ReadLine();

            //чтение данных из файла
            List<Student> student = ReadFileLine(nameFile);
            PrintStudent(student);

            //запись и чтение из файла
             var count = InputCountStu();

             List<Student> students =AddStudent(count);

             WriteFile(nameFile, students);

             List<Student> student1 = ReadFile(nameFile);

             Console.WriteLine(" ");
             Console.WriteLine("Информация, хранящаяся в файле: ");
             PrintStudent(student1);


             Console.WriteLine(" ");
             Console.WriteLine("Информация, хранящаяся в файле после удаления отличников: ");
             List<Student> student2 = DeleteStudent(students);
             PrintStudent(student2);

             Console.WriteLine(" ");
             Console.Write("Введите фамилию, после которой планируется добавить новую запись: ");
             string name= Console.ReadLine();
             int counts = 0;

             foreach(Student studentnew in student2)
             {
                 if (studentnew.FIO == name) { counts++; }
             }

             if (counts > 0)
             {
                 List<Student> student3 = AddNewStudent(student2, name);
                 PrintStudent(student3);
             }
             else { Console.WriteLine("Такой фамилии в списке нет!!!"); }

             

        }
    }
}