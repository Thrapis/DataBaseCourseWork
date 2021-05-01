using MobileOperatorApplication.Model;
using MobileOperatorApplication.Oracle;
using MobileOperatorApplication.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Data
{
	public static class DataGeneration
	{
		public static int GenerateAllData()
        {
			int generated = 0;

			Console.WriteLine("Generation of Posts");
			generated += GeneratePosts();
			Console.WriteLine("Generation of Tariff Plans");
			generated += GenerateTariffPlans();
			Console.WriteLine("Generation of Service Descriptions");
			generated += GenerateServiceDescriptions();
			Console.WriteLine("Generation of Clients");
			generated += GenerateClients(10000);
			Console.WriteLine("Generation of Employees");
			generated += GenerateEmployees(200);
			Console.WriteLine("Generation of Contracts");
			generated += GenerateContracts(12000);
			Console.WriteLine("Generation of Services");
			generated += GenerateServices(0, 7);
			Console.WriteLine("Generation of Phone Numbers");
			generated += GeneratePhoneNumbers();
			Console.WriteLine("Generation of Calls");
			generated += GenerateCalls(50000);
			Console.WriteLine("Generation of Payments");
			generated += GeneratePayments();

			return generated;
        }

		public static int GetAllDataCount()
        {
			int count = 0;
			count += new CallRepository().GetAll().Count();
			count += new ClientRepository().GetAll().Count();
			count += new ContractRepository().GetAll().Count();
			count += new DebitRepository().GetAll().Count();
			count += new EmployeeRepository().GetAll().Count();
			count += new PaymentRepository().GetAll().Count();
			count += new PhoneNumberRepository().GetAll().Count();
			count += new PostRepository().GetAll().Count();
			count += new ServiceDescriptionRepository().GetAll().Count();
			count += new ServiceRepository().GetAll().Count();
			count += new TariffPlanRepository().GetAll().Count();
			return count;
        }

		public static int GeneratePosts()
		{
			int inserted = 0;
			PostRepository repository = new PostRepository();

			inserted += repository.Insert(new Post("Senior manager", "First")) != -1 ? 1 : 0;
			inserted += repository.Insert(new Post("Cashier", "Second")) != -1 ? 1 : 0;
			inserted += repository.Insert(new Post("Manager", "Third")) != -1 ? 1 : 0;

			return inserted;
		}

		private static string GetRandomPassportNumber(Random rand)
		{
			string alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string nums = "0123456789";

			string result = "";
			result += alph[rand.Next(0, alph.Length)];
			result += alph[rand.Next(0, alph.Length)];
			result += nums[rand.Next(0, nums.Length)];
			result += nums[rand.Next(0, nums.Length)];
			result += nums[rand.Next(0, nums.Length)];
			result += nums[rand.Next(0, nums.Length)];
			result += nums[rand.Next(0, nums.Length)];
			result += nums[rand.Next(0, nums.Length)];
			result += nums[rand.Next(0, nums.Length)];

			return result;
		}

		public static int GenerateClients(int count)
		{
			string firstnames_path = @"E:\Study\BD_Course_Work\DataForGeneration\firstnames.txt";
			string lastnames_path = @"E:\Study\BD_Course_Work\DataForGeneration\lastnames.txt";
			string firstnames_string = "";
			string lastnames_string = "";
			int inserted = 0;
			using (StreamReader sr = new StreamReader(firstnames_path))
			{
				firstnames_string = sr.ReadToEnd();
			}
			using (StreamReader sr = new StreamReader(lastnames_path))
			{
				lastnames_string = sr.ReadToEnd();
			}
			firstnames_string = firstnames_string.Replace("\r\n", ";");
			lastnames_string = lastnames_string.Replace("\n", ";");
			List<string> firstnames_list = new List<string>(firstnames_string.Split(';'));
			List<string> lastnames_list = new List<string>(lastnames_string.Split(';'));
			Random rand = new Random();

			OracleProvider provider = new OracleProvider();
			ClientRepository repository = new ClientRepository();
			for (int i = 0; i < count; i++)
			{
				string firstname = firstnames_list[rand.Next(0, firstnames_list.Count)];
				string lastname = lastnames_list[rand.Next(0, lastnames_list.Count)];
				string fullname = firstname + " " + lastname;
				string login = firstname.ToLower() + "_" + lastname.ToLower();
				int ins = provider.CreateAccount(login, "12345", 1);
				while (ins != 1)
                {
					login += rand.Next(0, 10).ToString();
					ins = provider.CreateAccount(login, "12345", 1);
				}
				Client client = new Client(fullname, GetRandomPassportNumber(rand), login);
				inserted += repository.Insert(client) != -1 ? 1 : 0;
			}
			return inserted;
		}

		public static int GenerateTariffPlans()
		{
			int inserted = 0;

			TariffPlanRepository repository = new TariffPlanRepository();

			inserted += repository.Insert(new TariffPlan("Безлимитище+", 25.58f)) != -1 ? 1 : 0;
			inserted += repository.Insert(new TariffPlan("Супер 10", 21.50f)) != -1 ? 1 : 0;
			inserted += repository.Insert(new TariffPlan("Супер 25", 30.60f)) != -1 ? 1 : 0;
			inserted += repository.Insert(new TariffPlan("Супер", 10.81f)) != -1 ? 1 : 0;
			inserted += repository.Insert(new TariffPlan("Супер GO", 8.84f)) != -1 ? 1 : 0;
			inserted += repository.Insert(new TariffPlan("ULTRA", 55.09f)) != -1 ? 1 : 0;
			inserted += repository.Insert(new TariffPlan("Абсолют", 149.14f)) != -1 ? 1 : 0;
			inserted += repository.Insert(new TariffPlan("Легко сказать", 0.66f)) != -1 ? 1 : 0;
			inserted += repository.Insert(new TariffPlan("Особый", 1.23f)) != -1 ? 1 : 0;
			inserted += repository.Insert(new TariffPlan("Детский", 1.19f)) != -1 ? 1 : 0;
			inserted += repository.Insert(new TariffPlan("Близкий", 0.66f)) != -1 ? 1 : 0;
			inserted += repository.Insert(new TariffPlan("Победа", 1.00f)) != -1 ? 1 : 0;

			return inserted;
		}

		public static int GenerateServiceDescriptions()
		{
			int inserted = 0;
			ServiceDescriptionRepository repository = new ServiceDescriptionRepository();

			inserted += repository.Insert(new ServiceDescription("3 телефона", "3 номера телефона, которые позволят пользоваться вашим тарифным планом, указанным в договоре.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("USSD-расписание транспорта", "Сервис позволит вам получать информацию о времени до прибытия общественного транспорта на остановочны пункт.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Сервис «Parkme»", "Сервис предоставляет абонентам возможность совершить с помощью отправки SMS-сообщения или" +
				" в процессе пользования мобильным приложением юридически значимые действия, необходимые для заказа и оплаты временного возмездного пользования абонентом свободного места" +
				" на платной парковке.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Сервис «Парковка»", "Сервис предоставляет абонентам возможность оплатить парковку своего автомобиля с помощь отправки SMS-, USSD-" +
				" запроса или в процессе пользования мобильным приложением.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Семья под присмотром", "Благодаря услуге «Семья под присмотром» каждый родитель перестанет постоянно гадать, где сейчас находится" +
				" ребенок: в школе, на тренировке, на прогулке с друзьями или в гостях у бабушки. Услуга «Семья под присмотром» позволяет вам определять местоположение ребенка и узнавать, по какому адресу" +
				" он находится прямо сейчас.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Локатор", "Локатор — это простая возможность определения местоположения абонентов: твоих друзей и близких. Ты прямо сейчас можешь узнать," +
				" где находятся твои друзья и близкие.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Уроки английского", "Сервис «Портал по изучению английского языка позволяет абонентам при помощи простого SMS-интерфейса, WAP-интерфейса," +
				" а также приложения изучать английские слова, также абонентам предоставляется возможность изучение правил грамматики английского языка с помощью видеороликов на WAP-портале." +
				" Чтобы воспользоваться сервисом, вам необходимо оформить подписку.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Уроки русского", "Сервис «Уроки русского» поможет вам совершенствовать знания русского языка. Предусмотрена возможность использования" +
				" сервиса в качестве толкового словаря. Ежедневно сервис в простой игровой форме посредством SMS сообщений предлагает ответить на интересные вопросы, посвященные грамматике, лексике и фразеологии.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Уроки польского", "Сервис «Уроки польского» поможет вам совершенствовать знания польского языка и подготовится к" +
				" собеседованию на получение Карты поляка. Обучение производится при помощи простого SMS-интерфейса. Предусмотрена возможность использования сервиса в качестве переводчика." +
				" Вы можете самостоятельно выбрать тему обучения.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Родная мова", "Сервис «Родная мова» помогает абонентам совершенствовать знания белорусского языка. Изучение белорусских слов" +
				" производится при помощи простого SMS-интерфейса или WAP-интерфейса. Предусмотрена возможность использования сервиса в качестве переводчика.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("SMS-ИТОГИ", "SMS-ИТОГИ — услуга, позволяющая с помощью SMS-запроса получать результаты репетиционного и централизованного тестирования.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Премиум-контент в образовательных сервисах", "Услуга «Премиум-контент в образовательных сервисах» - это доступ к расширенному функционалу платформы." +
				" Данная платформа обрабатывает и предоставляет в удобном электронном виде информацию об успеваемости учащихся.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Баланс веса", "Сервис «Баланс веса» позволяет абонентам рассчитать индекс массы тела, выбрать подходящую для себя диету, а также получать" +
				" ежедневную информацию о пользе продуктов для здорового питания.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Баланс воды", "Сервис «Баланс воды» позволяет абоненту следить за уровнем воды в организме, исходя из индивидуальных параметров: роста," +
				" веса, возраста и физической активности. Сервис «Баланс воды» поможет контролировать водный баланс и превратит питье воды в полезную привычку.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Клуб скидок", "Услуга «Клуб скидок» —лучшие скидки города в Вашем телефоне: лучшие рестораны, СПА, салоны красоты," +
				" спортивные залы и многое другое со скидками до 90%. Подписчикам услуги не нужно покупать каждый купон по отдельности: пользоваться промокодами можно одновременно" +
				" и без всяких ограничений. Главное условие — еженедельная подписка.")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Объявления и реклама", "Размещай объявления в газетах, на радио, телеканалах и интернет-сайтах, а также получай подробную информацию" +
				" о рекламируемых посредством СМИ товарах (услугах).")) != -1 ? 1 : 0;
			inserted += repository.Insert(new ServiceDescription("Мобильные вакансии", "Услуга «Мобильные вакансии» заключается в предоставлении доступа к информации о новых вакансиях на Портале.")) != -1 ? 1 : 0;

			return inserted;
		}

		public static int GenerateEmployees(int count, float first_part = 0.1f, float second_part = 0.35f, float third_part = 0.55f)
		{
			int inserted = 0;

			string firstnames_path = @"E:\Study\BD_Course_Work\DataForGeneration\firstnames.txt";
			string lastnames_path = @"E:\Study\BD_Course_Work\DataForGeneration\lastnames.txt";
			string firstnames_string = "";
			string lastnames_string = "";
			using (StreamReader sr = new StreamReader(firstnames_path))
			{
				firstnames_string = sr.ReadToEnd();
			}
			using (StreamReader sr = new StreamReader(lastnames_path))
			{
				lastnames_string = sr.ReadToEnd();
			}
			firstnames_string = firstnames_string.Replace("\r\n", ";");
			lastnames_string = lastnames_string.Replace("\n", ";");
			List<string> firstnames_list = new List<string>(firstnames_string.Split(';'));
			List<string> lastnames_list = new List<string>(lastnames_string.Split(';'));
			Random rand = new Random();

			EmployeeRepository repository = new EmployeeRepository();
			OracleProvider provider = new OracleProvider();

			for (int i = 0; i < count * first_part; i++)
			{
				string firstname = firstnames_list[rand.Next(0, firstnames_list.Count)];
				string lastname = lastnames_list[rand.Next(0, lastnames_list.Count)];
				string fullname = firstname + " " + lastname;
				string login = firstname.ToLower() + "_" + lastname.ToLower();

				int ins = provider.CreateAccount(login, "Password3", 3);
				while (ins != 1)
				{
					login += rand.Next(0, 10).ToString();
					ins = provider.CreateAccount(login, "Password3", 3);
				}
				Employee employee = new Employee(fullname, 1, login);
				inserted += repository.Insert(employee) != -1 ? 1 : 0;
			}

			for (int i = 0; i < count * second_part; i++)
			{
				string firstname = firstnames_list[rand.Next(0, firstnames_list.Count)];
				string lastname = lastnames_list[rand.Next(0, lastnames_list.Count)];
				string fullname = firstname + " " + lastname;
				string login = firstname.ToLower() + "_" + lastname.ToLower();

				int ins = provider.CreateAccount(login, "Password2", 2);
				while (ins != 1)
				{
					login += rand.Next(0, 10).ToString();
					ins = provider.CreateAccount(login, "Password2", 2);
				}
				Employee employee = new Employee(fullname, 2, login);
				inserted += repository.Insert(employee) != -1 ? 1 : 0;
			}

			for (int i = 0; i < count * third_part; i++)
			{
				string firstname = firstnames_list[rand.Next(0, firstnames_list.Count)];
				string lastname = lastnames_list[rand.Next(0, lastnames_list.Count)];
				string fullname = firstname + " " + lastname;
				string login = firstname.ToLower() + "_" + lastname.ToLower();

				int ins = provider.CreateAccount(login, "Password1", 2);
				while (ins != 1)
				{
					login += rand.Next(0, 10).ToString();
					ins = provider.CreateAccount(login, "Password1", 2);
				}
				Employee employee = new Employee(fullname, 3, login);
				inserted += repository.Insert(employee) != -1 ? 1 : 0;
			}

			return inserted;
		}

		public static int GenerateContracts(int count)
		{
			int inserted = 0;
			
			IEnumerable<TariffPlan> tariff_plans = new TariffPlanRepository().GetAll();
			IEnumerable<Client> clients = new ClientRepository().GetAll();
			IEnumerable<Employee> employees = new EmployeeRepository().GetAll();
			
			ContractRepository repository = new ContractRepository();

			if (tariff_plans.Count() == 0 || employees.Count() == 0 || clients.Count() == 0)
				throw new Exception("Can't generate contracts");

			Random rand = new Random();

			for (int i = 0; i < count; i++)
			{
				TariffPlan tariff_plan = tariff_plans.ElementAt(rand.Next(0, tariff_plans.Count()));
				Client client = clients.ElementAt(rand.Next(0, clients.Count()));
				Employee employee = employees.ElementAt(rand.Next(0, employees.Count()));

				DateTime signing_datetime = DateTime.Now.AddDays(rand.Next(-720, 0));

				inserted += repository.Insert(new Contract(tariff_plan.ID, client.ID, employee.ID, signing_datetime)) != -1 ? 1 : 0;
			}

			return inserted;
		}

		public static int GenerateServices(int min_serv_count = 0, int max_serv_count = 5)
		{
			int inserted = 0;

			IEnumerable<Contract> contracts = new ContractRepository().GetAll();
			IEnumerable<ServiceDescription> serviceDescriptions = new ServiceDescriptionRepository().GetAll();

			if (contracts.Count() == 0 || serviceDescriptions.Count() == 0)
				throw new Exception("Can't generate services");

			ServiceRepository repository = new ServiceRepository();

			Random rand = new Random();

			for (int i = 0; i < contracts.Count(); i++)
			{
				Contract contract = contracts.ElementAt(i);

				SortedSet<int> service_descriptions = new SortedSet<int>();
				int serv_count = rand.Next(min_serv_count, max_serv_count + 1);

				while (service_descriptions.Count < serv_count)
				{
					service_descriptions.Add(serviceDescriptions.ElementAt(rand.Next(0, serviceDescriptions.Count())).ID);
				}

				for (int j = 0; j < service_descriptions.Count; j++)
				{
					float amount = (float)(rand.NextDouble() * 5 + 2.5);
					DateTime disconnect = DateTime.Now.AddDays(rand.NextDouble() * 90 + 1);
					DateTime connect = disconnect.AddMonths(-3);
					Service service = new Service(contract.ID, service_descriptions.ElementAt(j), amount, connect, disconnect);

					inserted += repository.Insert(service) != -1 ? 1 : 0;
				}       
			}

			return inserted;
		}

		private static string GetRandomPhoneNumber(Random rand)
		{
			string nums = "0123456789";

			string result = "+375";
			
			for (int i = 0; i < 9; i++)
			{
				result += nums[rand.Next(0, nums.Length)];
			}

			return result;
		}

		public static int GeneratePhoneNumbers()
		{
			int inserted = 0;

			IEnumerable<Contract> contracts = new ContractRepository().GetAll();
			IEnumerable<Service> services = new ServiceRepository().GetAll();

			PhoneNumberRepository repository = new PhoneNumberRepository();

			if (contracts.Count() == 0 || services.Count() == 0)
				throw new Exception("Can't generate phone numbers");

			Random rand = new Random();

			for (int i = 0; i < contracts.Count(); i++)
			{
				int temp_insert = 0;

				for (int j = 0; j < services.Count(); j++)
				{
					if (contracts.ElementAt(i).ID == services.ElementAt(j).ID)
					{
						if (services.ElementAt(j).DESCRIPTION_ID == 1)
						{
							while (temp_insert < 3)
								temp_insert += repository.Insert(new PhoneNumber(GetRandomPhoneNumber(rand), contracts.ElementAt(i).ID)) != -1 ? 1 : 0;

							break;
						}
					}
				}

				while (temp_insert < 1)
					temp_insert += repository.Insert(new PhoneNumber(GetRandomPhoneNumber(rand), contracts.ElementAt(i).ID));

				inserted += temp_insert;
			}

			return inserted;
		}

		public static int GenerateCalls(int count)
		{
			int inserted = 0;

			IEnumerable<Contract> contracts = new ContractRepository().GetAll();

			if (contracts.Count() == 0)
				throw new Exception("Can't generate calls");

			CallRepository repository = new CallRepository();

			Random rand = new Random();

			for (int i = 0; i < count; i++)
			{
				Contract contract = contracts.ElementAt(rand.Next(0, contracts.Count()));

				int contact_id = contract.ID;
				TimeSpan talk_time = new TimeSpan(0, rand.Next(0, 24), rand.Next(0, 60), rand.Next(0, 60));
				long ticks = (long)(rand.NextDouble() * (double)(DateTime.Now.Ticks - contract.SIGNING_DATETIME.Ticks));
				DateTime call_dateTime = contract.SIGNING_DATETIME.AddTicks(ticks);

				inserted += repository.Insert(new Call(contact_id, GetRandomPhoneNumber(rand), talk_time, call_dateTime)) != -1 ? 1 : 0;
			}
			
			return inserted;
		}

		public static int GeneratePayments(int max_payments_count = 6, float min_payments = 2.5f, float max_payments = 140f)
		{
			int inserted = 0;

			IEnumerable<Contract> contracts = new ContractRepository().GetAll();

			if (contracts.Count() == 0)
				throw new Exception("Can't generate calls");

			PaymentRepository repository = new PaymentRepository();

			Random rand = new Random();

			for (int i = 0; i < contracts.Count(); i++)
			{
				Console.WriteLine("Contract payments: " + i);
				Contract contract = contracts.ElementAt(i);

				int payments_count = rand.Next(1, max_payments_count);

				for (int j = 0; j < payments_count; j++)
                {
					float payment = (float)rand.NextDouble() * (max_payments - min_payments) + min_payments;
					long ticks = (long)(rand.NextDouble() * (double)(DateTime.Now.Ticks - contract.SIGNING_DATETIME.Ticks));
					DateTime payment_datetime = contract.SIGNING_DATETIME.AddTicks(ticks);

					inserted += repository.Insert(new Payment(contract.ID, payment, payment_datetime)) != -1 ? 1 : 0;
				}
			}

			return inserted;
		}
	}
}
