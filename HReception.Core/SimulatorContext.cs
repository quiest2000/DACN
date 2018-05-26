using HReception.Core.Context.EfModels;
using Microsoft.EntityFrameworkCore;
using FreshMvvm;
using HReception.Core.Context.Infrastructure;

namespace Client.UI.Infrastructure.Context
{
    public class SimulatorContext : DbContext
    {
        private static string _dbPath = null;
        private static bool _isDbFileCreated = false;

        private SimulatorContext()
        {
            Database.Migrate();
            Database.EnsureCreated();
        }

        public static SimulatorContext CreateContext()
        {
            var helper = FreshIOC.Container.Resolve<IDbHelper>();
            _dbPath = helper.GetDbPath();
            _isDbFileCreated = helper.IsDbFileCreated();
            var context = new SimulatorContext();
            if (_isDbFileCreated)
                return context;
            SeekData(context);
            return context;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"FileName={_dbPath}");
        }
        #region DbSets

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }
        public virtual DbSet<Gencode> Gencodes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        #endregion

        static void SeekData(SimulatorContext context)
        {
            //todo: seek data
        }
    }

    //public class SimulatorInitializer : CreateDatabaseIfNotExists<SimulatorContext>
    //{
    //    protected override void Seed(SimulatorContext context)
    //    {
    //        base.Seed(context);
    //        var securityService = ServiceLocator.Default.ResolveType<ISecurityService>();
    //        var generator = ServiceLocator.Default.ResolveType<IGenerator>();
    //        context.Gencodes.AddRange(new[]
    //        {
    //            new Gencode
    //            {
    //                EntityTypeFullName = typeof(Patient).FullName,
    //                Reset = ResetUnit.Yearly,
    //                Prefix = "BN",
    //            },
    //            new Gencode
    //            {
    //                EntityTypeFullName = typeof(User).FullName,
    //                Reset = ResetUnit.Yearly,
    //                Prefix = "NV",
    //            }
    //        });
    //        context.SaveChanges();
    //        //patient
    //        var patients = new List<Patient>
    //        {
    //            new Patient
    //            {
    //                PatientCode = generator.Next<Patient>(),
    //                FullName = "Triệu Thanh Thanh",
    //                DoB = "01/09/1993",
    //                Email = "thanhtrieu93@gmail.com",
    //                FullAddress = "0901. IndoChina Park, 04, Nguyen Dinh Chieu, Dakao, Q1, HCMC",
    //                Gender = "Nữ",
    //                Phone = "09784564515",
    //            },
    //            new Patient
    //            {
    //                PatientCode = generator.Next<Patient>(),
    //                FullName = "Trần Mỹ Hoa",
    //                DoB = "07/12/1991",
    //                Email = "myhoa93@gmail.com",
    //                FullAddress = "480, Xo Viet Nghe Tinh, Binh Thanh, HCMC",
    //                Gender = "Nữ",
    //                Phone = "09194565545",
    //            }
    //        };
    //        context.Patients.AddRange(patients);

    //        //items
    //        var items = new List<Item>
    //        {
    //            new Item
    //            {
    //                ItemCode = "001",
    //                ItemName = "Chụp X Quang",
    //                UnitName = "Lần",
    //                UnitPrice = 120000,
    //            },
    //            new Item
    //            {
    //                ItemCode = "002",
    //                ItemName = "Sieu âm 2D",
    //                UnitName = "Lần",
    //                UnitPrice = 80000,
    //            },
    //            new Item
    //            {
    //                ItemCode = "KhamNoiTq",
    //                ItemName = "Khám nội tổng quát",
    //                UnitName = "Lần",
    //                UnitPrice = 120000,
    //            },
    //            new Item
    //            {
    //                ItemCode = "KhamNoiTm",
    //                ItemName = "Khám nội tim mạch",
    //                UnitName = "Lần",
    //                UnitPrice = 120000,
    //            },
    //            new Item
    //            {
    //                ItemCode = "KhamNoiTh",
    //                ItemName = "Khám nội tiêu hóa",
    //                UnitName = "Lần",
    //                UnitPrice = 120000,
    //            },
    //            new Item
    //            {
    //                ItemCode = "KhamVg",
    //                ItemName = "Khám viêm gan",
    //                UnitName = "Lần",
    //                UnitPrice = 120000,
    //            },
    //            new Item
    //            {
    //                ItemCode = "KhamNgoaiTq",
    //                ItemName = "Khám ngoại tổng quát",
    //                UnitName = "Lần",
    //                UnitPrice = 120000,
    //            },
    //            new Item
    //            {
    //                ItemCode = "KhamNhi",
    //                ItemName = "Khám nhi khoa",
    //                UnitName = "Lần",
    //                UnitPrice = 120000,
    //            },
    //            new Item
    //            {
    //                ItemCode = "KhamThai",
    //                ItemName = "Khám thai",
    //                UnitName = "Lần",
    //                UnitPrice = 120000,
    //            },
    //            new Item
    //            {
    //                ItemCode = "KhamPhu",
    //                ItemName = "Khám phụ khoa",
    //                UnitName = "Lần",
    //                UnitPrice = 120000,
    //            }
    //        };
    //        context.Items.AddRange(items);
    //        var users = new List<User>()
    //        {
    //            new User
    //            {
    //                UserName = "nv1",
    //                FullName = "Nguyễn Hồng Hạnh",
    //                Roles = new[] {RoleNames.Employee}.Separate()
    //            },
    //            new User
    //            {
    //                UserName = "nv2",
    //                FullName = "Trần Minh Ngọc",
    //                Roles = new[] {RoleNames.Employee}.Separate()
    //            },
    //            new User
    //            {
    //                UserName = "ql1",
    //                FullName = "Lê Thị Tố Loan",
    //                Roles = new[] {RoleNames.Employee, RoleNames.Manager}.Separate()
    //            }
    //        };
    //        users.ForEach(user =>
    //        {
    //            user.Code = generator.Next<User>();
    //            user.IsActive = true;
    //            user.Salt = securityService.CreateSalt();
    //            user.Password = securityService.Hash("123456", user.Salt);
    //        });
    //        context.Users.AddRange(users);

    //        context.SaveChanges();
    //    }
    //}
}