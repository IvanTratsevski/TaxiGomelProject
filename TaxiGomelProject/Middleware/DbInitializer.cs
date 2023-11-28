using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing.Text;
using TaxiGomelProject.Data;
using TaxiGomelProject.Models;
namespace TaxiGomelProject.Middleware
{
    public class DbInitializer
    {
        public static void Initialize(TaxiGomelContext db)
        {
            db.Database.EnsureCreated();


            int single_link = 50;
            int many_link = 100;
            Random random = new Random(1);
            string[] car_models = { "BMW", "Toyota", "Honda", "Lada", "Ford", "Volvo", "Skoda" };
            string[] tech_stats = { "Объем бака", "Время разгона", "Мощность", "Ход поршня", "Привод" };
            string[] specifications = { "M5", "Granta", "M2", "Gran Coupe", "Aspire", "8" };

            string[] first_names_m = { "Кирил", "Михаил", "Андрей", "Александр", "Иван", "Олег" };
            string[] last_names_m = { "Нарыжкин", "Цаль", "Попов", "Сидоров", "Тихонов", "Гордей" };

            string[] first_names_fm = { "Ольга", "Виктория", "Оксана", "Светалана", "Наталья" };
            string[] last_names_fm = { "Нарыжкина", "Попова", "Сидорова", "Тихонова", "Мацкевич" };

            string[] streets = { "Советская", "Героев-Подпольщиков", "Рябиновая", "Школьная", "Жукова" };

            string[] special_marks = { "Бита", "Крашена", "Не бита", "Не крашена" };

            string[] rates = { "Эконом", "Комфорт", "Детский", "Экспресс", "Стандарт" };
            string[] GetRandomFullName()
            {
                if (random.Next(0, 2) == 1)
                {
                    string[] result ={ first_names_m[random.Next(0, first_names_m.Length)], last_names_m[random.Next(0, last_names_m.Length)] };
                    return result;
                } else
                {
                    string[] result = { first_names_fm[random.Next(0, first_names_fm.Length)], last_names_fm[random.Next(0, last_names_fm.Length)] };
                    return result;
                }
            }
            string GetRandomNumbers(int length)
            {
                var random = new Random();
                string s = string.Empty;
                for (int i = 0; i < length; i++)
                    s = String.Concat(s, random.Next(10).ToString());
                return s;
            }
            DateTime GetRandomDate()
            {
                DateTime start = new DateTime(1995, 1, 1);
                int range = (DateTime.Today - start).Days;
                return start.AddDays(random.Next(range));
            }
            string GetRandomAdress()
            {
                return streets[random.Next(0, streets.Length)] + random.Next(0, 70).ToString();
            }
            if (!db.CarModels.Any())
            {
                for (int i = 0; i < single_link; i++)
                {
                    db.CarModels.Add(new CarModel
                    {
                        ModelName = car_models[random.Next(0, car_models.Length)],
                        TechStats = tech_stats[random.Next(0, tech_stats.Length)],
                        Price = random.Next(800, 4000),
                        Specifications = specifications[random.Next(0, specifications.Length)]
                    });
                }
                db.SaveChanges();
            }

            if (!db.Positions.Any())
            {
                db.Positions.Add(new Position
                {
                    PositionName = "Водитель",
                    Salary = 700
                });
                db.Positions.Add(new Position
                {
                    PositionName = "Механик",
                    Salary = 1100
                });
                db.Positions.Add(new Position
                {
                    PositionName = "Диспетчер",
                    Salary = 650
                });
                db.SaveChanges();
            }

            if (!db.Rates.Any())
            {
                for (int i = 0; i < single_link; i++)
                {
                    db.Rates.Add(new Rate
                    {
                        RateDescription = rates[random.Next(0, rates.Length)],
                        RatePrice = random.Next(1, 6)
                    });
                }
                db.SaveChanges();
            }


            if (!db.Employees.Any())
            {
                string[] rndfullname_driver = GetRandomFullName();
                // создание трех содрудников чтобы гарантиовано иметь как минимум по однмоу сотруднику каждой специальности
                //это необходимо т.к. в базе данных существую триггеры ограничения которые не позволят записать например механика на пост диспетчера
                db.Employees.Add(new Employee
                {
                    FirstName = rndfullname_driver[0],
                    LastName = rndfullname_driver[1],
                    Age = random.Next(18, 100),
                    PositionId = 1,
                    Experience = random.Next(0, 100)
                });
                string[] rndfullname_mechanic = GetRandomFullName();
                db.Employees.Add(new Employee
                {
                    FirstName = rndfullname_mechanic[0],
                    LastName = rndfullname_mechanic[1],
                    Age = random.Next(18, 100),
                    PositionId = 2,
                    Experience = random.Next(0, 100)
                });
                string[] rndfullname_dispatcher = GetRandomFullName();
                db.Employees.Add(new Employee
                {
                    FirstName = rndfullname_dispatcher[0],
                    LastName = rndfullname_dispatcher[1],
                    Age = random.Next(18, 100),
                    PositionId = 3,
                    Experience = random.Next(0, 100)
                });
                string[] rndfullname;
                for (int i = 0; i < many_link; i++)
                {
                    rndfullname = GetRandomFullName();
                    db.Employees.Add(new Employee
                    {
                        FirstName = rndfullname[0],
                        LastName = rndfullname[1],
                        Age = random.Next(18, 100),
                        PositionId = random.Next(1, 4),
                        Experience = random.Next(0, 100)
                    });
                }
                db.SaveChanges();
            }
            if (!db.Cars.Any())
            {
                for (int i = 0; i < many_link; i++)
                {
                    db.Cars.Add(new Car
                    {
                        RegistrationNumber = GetRandomNumbers(7),
                        CarModelId = random.Next(1, single_link),
                        CarcaseNumber = GetRandomNumbers(11),
                        EngineNumber = GetRandomNumbers(11),
                        ReleaseYear = GetRandomDate(),
                        Mileage = random.Next(0, 100000),
                        LastTi = GetRandomDate(),
                        SpecialMarks = special_marks[random.Next(0, special_marks.Length)]
                    });
                }
                db.SaveChanges();
            }
            //Я частично разобрался почему не добавлялись записи, дело было в том что при заполнении одной из табли CarsDrivers или CarsMechanics
            //вторую таблицу невозможно заполнить, однако я так и не смог выяснить в чем конфликт
            if (!db.CarDrivers.Any())
            {
                for (int i = 0; i < many_link; i++)
                {
                    db.CarDrivers.Add(new CarDriver
                    {
                        CarId = random.Next(1, many_link),
                        DriverId = 1,
                    });
                }
                db.SaveChanges();
            }

            if (!db.Calls.Any())
            {
                //Пробелма состоит в том, что при попытке указать значени полей Call, они считываются как null
                //т.е. я пытаюсь записать например в поле Telephone некое значение,
                //но при отправке запроса выдается ошибка, что невозможно записать в поле Telephone значение null
                //каким-то образом строка с данными не отправляется в запросе
                //при чем DispatcherId отправляется корректно
                string telephone = "1241424";
                for (int i = 0; i < many_link; i++)
                {
                    db.Calls.Add(new Call
                    {
                        Telephone = telephone,
                        RateId=1,CarId=1,
                        DispatcherId=3
                    });
                }
                db.SaveChanges();
            }
            if (!db.CarMechanics.Any())
            {
                for (int i = 0; i < many_link; i++)
                {
                    db.CarMechanics.Add(new CarMechanic
                    {
                        CarId = random.Next(1, many_link),
                        MechanicId = 2
                    });
                }
                db.SaveChanges();
            }

        }
    }
}
