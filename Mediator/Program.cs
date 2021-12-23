using System;
using System.Collections.Generic;

namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            Mediator mediator = new Mediator();

            Teacher cahit = new Teacher(mediator);
            cahit.Name = "Cahit";

            mediator.Teacher = cahit;

            Student hakem = new Student(mediator);
            hakem.Name = "Hakem";

            Student salih = new Student(mediator);
            salih.Name = "Salih";

            mediator.Students = new List<Student> { hakem, salih };

            cahit.SendNewImageUrl("slide1.jpg");

            cahit.RecieveQuestion("is it true?", hakem);



            Console.ReadKey();
        }
    }

    abstract class CourseMember
    {
        protected Mediator Mediator;
        public CourseMember(Mediator mediator)
        {
            Mediator = mediator;
        }
    }

    class Teacher : CourseMember
    {
        public Teacher(Mediator mediator) : base(mediator)
        {
        }

        public void RecieveQuestion(string question, Student student)
        {
            Console.WriteLine("Teacher recieved a question from {0},{1}", student.Name, question);
        }

        public void SendNewImageUrl(string url)
        {
            Console.WriteLine("Teacher changed slide : {0}", url);
            Mediator.UpdateImage(url);
        }

        public void AnswerQuestion(string answer, Student student)
        {
            Console.WriteLine("Teacher answered question {0}, {1}", student.Name, answer);
        }

        public string Name { get; set; }
    }

    class Student : CourseMember
    {
        public Student(Mediator mediator) : base(mediator)
        {
        }

        public void RecieveImage(string url)
        {
            Console.WriteLine("{1} received image : {0}", url, Name);
        }

        public void RecieveAnswer(string answer)
        {
            Console.WriteLine("Student received answer {0}", answer);
        }

        public string Name { get; set; }
    }

    class Mediator
    {
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }

        public void UpdateImage(string url)
        {
            foreach (var student in Students)
            {
                student.RecieveImage(url);
            }
        }

        public void SendQuestion(string question, Student student)
        {
            Teacher.RecieveQuestion(question, student);
        }

        public void SendAnswer(string answer, Student student)
        {
            student.RecieveAnswer(answer);
        }
    }
}
