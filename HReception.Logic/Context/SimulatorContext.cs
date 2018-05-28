using System.Collections.Generic;
using FreshMvvm;
using HReception.Logic.Context.EfModels;
using HReception.Logic.Context.Enum;
using HReception.Logic.Context.Infrastructure;
using HReception.Logic.Services.Interfaces.Common;
using HReception.Logic.Utils.Extensions;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace HReception.Logic.Context
{
    public class SimulatorContext : DbContext
    {
        private const string DataInstalledKey = "SimulatorContext.DataInstalledKey";
        private static string _dbPath = null;
        private static readonly object _lockObj = new object();
        private SimulatorContext()
        {
            Database.EnsureCreated();
            Database.Migrate();
        }

        public static SimulatorContext Create()
        {
            if (string.IsNullOrEmpty(_dbPath))
            {
                var helper = FreshIOC.Container.Resolve<IDbHelper>();
                _dbPath = helper.GetDbPath();
            }

            var context = new SimulatorContext();
            var installed = false;
            lock (_lockObj)
            {
                installed = Application.Current.Properties.ContainsKey(DataInstalledKey);
            }

            if (!installed)
            {
                lock (_lockObj)
                {
                    Application.Current.Properties.Add(DataInstalledKey, true);
                    Application.Current.SavePropertiesAsync();
                }

                EnsureDataInstalled(context);
            }

            return context;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"FileName={_dbPath}");
        }
        #region DbSets

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<Gencode> Gencodes { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion

        static void EnsureDataInstalled(SimulatorContext context)
        {
            context.Gencodes.AddRange(new[]
            {
                        new Gencode
                        {
                            EntityTypeFullName = typeof(Patient).FullName,
                            Reset = ResetUnit.Yearly,
                            Prefix = "BN",
                        },
                        new Gencode
                        {
                            EntityTypeFullName = typeof(User).FullName,
                            Reset = ResetUnit.Yearly,
                            Prefix = "NV",
                        }
                    });
            context.SaveChanges();

            var securityService = FreshIOC.Container.Resolve<ISecurityService>();
            var generator = FreshIOC.Container.Resolve<IGenerator>();
            //patient
            var patients = new List<Patient>
                    {
                        new Patient
                        {
                            PatientCode = generator.Next<Patient>(),
                            FullName = "Triệu Thanh Thanh",
                            DoB = "01/09/1993",
                            Email = "thanhtrieu93@gmail.com",
                            FullAddress = "0901. IndoChina Park, 04, Nguyen Dinh Chieu, Dakao, Q1, HCMC",
                            Gender = "Nữ",
                            Phone = "09784564515",
                        },
                        new Patient
                        {
                            PatientCode = generator.Next<Patient>(),
                            FullName = "Trần Mỹ Hoa",
                            DoB = "07/12/1991",
                            Email = "myhoa93@gmail.com",
                            FullAddress = "480, Xo Viet Nghe Tinh, Binh Thanh, HCMC",
                            Gender = "Nữ",
                            Phone = "09194565545",
                        }
                    };
            context.Patients.AddRange(patients);
            //items
            var items = new List<Item>
                    {
                        new Item
                        {
                            ItemCode = "001",
                            ItemName = "Chụp X Quang",
                            UnitName = "Lần",
                            UnitPrice = 120000,
                        },
                        new Item
                        {
                            ItemCode = "002",
                            ItemName = "Sieu âm 2D",
                            UnitName = "Lần",
                            UnitPrice = 80000,
                        },
                        new Item
                        {
                            ItemCode = "KhamNoiTq",
                            ItemName = "Khám nội tổng quát",
                            UnitName = "Lần",
                            UnitPrice = 120000,
                        },
                        new Item
                        {
                            ItemCode = "KhamNoiTm",
                            ItemName = "Khám nội tim mạch",
                            UnitName = "Lần",
                            UnitPrice = 120000,
                        },
                        new Item
                        {
                            ItemCode = "KhamNoiTh",
                            ItemName = "Khám nội tiêu hóa",
                            UnitName = "Lần",
                            UnitPrice = 120000,
                        },
                        new Item
                        {
                            ItemCode = "KhamVg",
                            ItemName = "Khám viêm gan",
                            UnitName = "Lần",
                            UnitPrice = 120000,
                        },
                        new Item
                        {
                            ItemCode = "KhamNgoaiTq",
                            ItemName = "Khám ngoại tổng quát",
                            UnitName = "Lần",
                            UnitPrice = 120000,
                        },
                        new Item
                        {
                            ItemCode = "KhamNhi",
                            ItemName = "Khám nhi khoa",
                            UnitName = "Lần",
                            UnitPrice = 120000,
                        },
                        new Item
                        {
                            ItemCode = "KhamThai",
                            ItemName = "Khám thai",
                            UnitName = "Lần",
                            UnitPrice = 120000,
                        },
                        new Item
                        {
                            ItemCode = "KhamPhu",
                            ItemName = "Khám phụ khoa",
                            UnitName = "Lần",
                            UnitPrice = 120000,
                        }
                    };
            context.Items.AddRange(items);
            var users = new List<User>()
                    {
                        new User
                        {
                            UserName = "nv1",
                            FullName = "Nguyễn Hồng Hạnh",
                            Roles = new[] {RoleNames.Employee}.Separate()
                        },
                        new User
                        {
                            UserName = "nv2",
                            FullName = "Trần Minh Ngọc",
                            Roles = new[] {RoleNames.Employee}.Separate()
                        },
                        new User
                        {
                            UserName = "ql1",
                            FullName = "Lê Thị Tố Loan",
                            Roles = new[] {RoleNames.Employee, RoleNames.Manager}.Separate()
                        }
                    };
            users.ForEach(user =>
            {
                user.Code = generator.Next<User>();
                user.IsActive = true;
                user.Salt = securityService.CreateSalt();
                user.Password = securityService.Hash("123456", user.Salt);
            });
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}