using System;
using System.Text;

namespace mcq
{
    class Program
    {
        static void Main(string[] args)
        {
            Exam exam = new Exam();
            String url = "az900.txt";
            String passkey = "READYFORAZURE";
            //Console.WindowWidth = 100;  // 0 < Console.WindowWidth <= Console.LargestWindowWidth
            Console.Title = "Practice Test Application For Certification Exams | TLS-ET";

            // Opening message
            if (!exam.LoadQuestions(url))
            {
                Console.WriteLine("\nPress any key to quit...");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Welcome to AZ-900 Practice Test from TLS-ET");
                GetStringInput("Enter your GID: ");

                string password = GetMaskedStringInput("Enter TEST KEY: ");
                //Console.WriteLine(password);

                if (password.Equals(passkey))
                {
                    Console.WriteLine("Login Success!");
                    ClearAndContinue();

                }
                else
                {
                    Console.WriteLine("Sorry! Invalid key entered");
                    ClearAndContinue();
                    return;
                }
            }

            // Iterate through questions
            while (exam.MoveNext())
            {
                Console.WriteLine("AZ-900 Practice Test from TLS-ET");
                WriteHorizontalRule();

                BaseQuestion question = exam.CurrentQuestion;
                question.WriteQuestion();
                Console.Write("\nYour choice? ");

                int choice = 0;



                // Validate input
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > exam.CurrentQuestion.NumChoices)
                {
                    Console.WriteLine("Not a valid choice. Try again");
                }

                // Check answer
                if (exam.CheckAnswer(choice))
                {
                    Console.WriteLine("\nCorrect!");
                }
                else
                {
                    Console.WriteLine("\nWrong!");
                    Console.WriteLine($"Correct answer is: {question.Answer}");
                }

                WriteHorizontalRule();

                Console.WriteLine($"Your Score:  {exam.Average} %");
                Console.WriteLine($"# Correct:   {exam.QuestionsCorrect}");
                Console.WriteLine($"# Attempted: {exam.QuestionsCompleted}");

                ClearAndContinue();
            }

            WriteHorizontalRule();



            // Finish exam
            Console.WriteLine("You finished with " + exam.Average + "%\n");
            ClearAndContinue();
        }

        /// <summary>
        /// Write a sequence of -'s to create a horizontal line across the console
        /// </summary>
        private static void WriteHorizontalRule()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }

            Console.Write("\n");
        }

        private static string GetStringInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        private static string GetMaskedStringInput(string prompt)
        {
            Console.Write(prompt);
            StringBuilder passwordBuilder = new StringBuilder();
            bool continueReading = true;
            char newLineChar = '\r';
            while (continueReading)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                char passwordChar = consoleKeyInfo.KeyChar;
                Console.Write("*");
                if (passwordChar.Equals(newLineChar) || passwordChar.Equals('\n'))
                {
                    continueReading = false;
                }
                else
                {
                    passwordBuilder.Append(passwordChar.ToString());
                }
            }
            Console.WriteLine();
            return passwordBuilder.ToString();
        }

        private static void ClearAndContinue()
        {
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
