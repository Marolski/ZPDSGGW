using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Database;
using ZPDSGGW.Enums;
using ZPDSGGW.Models;
using System.Security.Cryptography;
using System.Text;

namespace ZPDSGGW.Data
{
    public class DbInitializer
    {
        public static String sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
        public static void Initialize(ZPDSGGWContext context, IServiceProvider services)
        {
            var logger = services.GetRequiredService<ILogger<DbInitializer>>();
            context.Database.EnsureCreated();
            SHA256 hash = SHA256.Create();
            // Look for any users.
            if (context.User.Any())
            {
                logger.LogInformation("Baza jest zasilona");
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{Id= Guid.Parse("a06c28f0-829b-481a-9ade-a10fa74a63b8"), Name ="Marcin", Surname="Student1", StudentNumber="123456", Username="student1",   Password=sha256_hash("1qaz@WSX"), Role="Student" },
            new User{Id= Guid.Parse("89e40023-1a9a-41b0-b290-28762d19b5d8"), Name ="Kacper", Surname="Student2", StudentNumber="145823", Username="student2",   Password=sha256_hash("1qaz@WSX"), Role="Student" },
            new User{Id= Guid.Parse("37b17861-bae6-4708-a007-bb5e3461c19b"), Name ="Maciek", Surname="Student3", StudentNumber="156985", Username="student3",   Password=sha256_hash("1qaz@WSX"), Role="Student" },
            new User{Id= Guid.Parse("bef9224e-1e52-4fda-b50a-3f4c0e94986b"), Name ="Bartosz", Surname="Student4", StudentNumber="236985", Username="student4",  Password=sha256_hash("1qaz@WSX"), Role="Student" },
            new User{Id= Guid.Parse("d5a848b3-5d2e-4b90-b920-35512d868aff"), Name ="Jan", Surname="Student5", StudentNumber="269843", Username="student5",      Password=sha256_hash("1qaz@WSX"), Role="Student" },
            new User{Id= Guid.Parse("1f2868a9-f14c-425b-847f-7e731ac3dacc"), Name ="Antoni", Surname="Student6", StudentNumber="232694", Username="student6",   Password=sha256_hash("1qaz@WSX"), Role="Student" },
            new User{Id= Guid.Parse("1a131ef5-3ce9-462e-9b45-c6817dccd7e8"), Name ="Michał", Surname="Student7", StudentNumber="129658", Username="student7",   Password=sha256_hash("1qaz@WSX"), Role="Student" },
            new User{Id= Guid.Parse("6891709b-9c35-4b47-bcb5-a2f917466705"), Name ="Marek", Surname="Student8", StudentNumber="369517", Username="student8",    Password=sha256_hash("1qaz@WSX"), Role="Student" }
            };
            foreach (User s in users)
            {
                context.User.Add(s);
            }
            context.SaveChanges();

            var promoters = new User[]
            {
            new User{Id= Guid.Parse("42575545-8931-4f10-9fb6-7a17ef2915b9"), Name ="Andrzej", Surname="Promotor1", Username="promotor1",Password=sha256_hash("1qaz@WSX"), Role="Promoter", Availability= 0, Degrees = Degrees.dr },
            new User{Id= Guid.Parse("3a627291-c9fc-4e48-8ee8-e966edf27318"), Name ="Kacper", Surname="Promotor2", Username="promotor2", Password=sha256_hash("1qaz@WSX"), Role="Promoter", Availability= 0, Degrees = Degrees.drHab },
            new User{Id= Guid.Parse("5b6b3673-c6e1-4565-9d0b-65fedf905794"), Name ="Karol", Surname="Promotor3", Username="promotor3",  Password=sha256_hash("1qaz@WSX"), Role="Promoter", Availability= 0, Degrees = Degrees.drMgr },
            new User{Id= Guid.Parse("734c295c-3ebe-478c-b129-7c95e108dc4c"), Name ="Bartosz", Surname="Promotor4", Username="promotor4",Password=sha256_hash("1qaz@WSX"), Role="Promoter", Availability= 0, Degrees = Degrees.prof },
            new User{Id= Guid.Parse("fb0f1566-cb66-48e4-8428-aa0f486ad579"), Name ="Jan", Surname="Promotor5", Username="promotor5",    Password=sha256_hash("1qaz@WSX"), Role="Promoter", Availability= 0, Degrees = Degrees.drInz },
            new User{Id= Guid.Parse("7d89e79c-004c-417f-9320-ca4db26f633f"), Name ="Antoni", Surname="Promotor6", Username="promotor6", Password=sha256_hash("1qaz@WSX"), Role="Promoter", Availability= 0, Degrees = Degrees.prof },
            new User{Id= Guid.Parse("0df0bbd9-2303-40b1-8daf-a3ac7d3d02a8"), Name ="Michał", Surname="Promotor7", Username="promotor7", Password=sha256_hash("1qaz@WSX"), Role="Promoter", Availability= 0, Degrees = Degrees.mgr },
            new User{Id= Guid.Parse("4b0dd23c-b78e-436a-8971-bd82e1684d5d"), Name ="Marek", Surname="Promotor8", Username="promotor8",  Password=sha256_hash("1qaz@WSX"), Role="Promoter", Availability= 0, Degrees = Degrees.dr }
            };
            foreach (User c in promoters)
            {
                context.User.Add(c);
            }
            context.SaveChanges();

            var topics = new ThesisTopic[]
            {
            new ThesisTopic{Id= Guid.Parse("7926ed52-83f4-416c-af09-bb9e09ea2d34"),PromoterId=Guid.Parse("42575545-8931-4f10-9fb6-7a17ef2915b9"), Available= ThesisTopicStatus.available, Topic="Wtyczka do chrome - implemetacja"},
            new ThesisTopic{Id= Guid.Parse("397fa1d6-4bf5-44d3-9855-e548c725af22"),PromoterId=Guid.Parse("3a627291-c9fc-4e48-8ee8-e966edf27318"), Available= ThesisTopicStatus.available, Topic="System do obsługi prac dyplomowych"},
            new ThesisTopic{Id= Guid.Parse("df9ea7da-09fe-4a53-9bee-747eff1d2a54"),PromoterId=Guid.Parse("5b6b3673-c6e1-4565-9d0b-65fedf905794"), Available= ThesisTopicStatus.available, Topic="System zarządzania apteką"},
            new ThesisTopic{Id= Guid.Parse("87946bf4-9df9-41cd-9093-67e5d0a55fbf"),PromoterId=Guid.Parse("734c295c-3ebe-478c-b129-7c95e108dc4c"), Available= ThesisTopicStatus.available, Topic="apliakcja do finansów domowych"},
            new ThesisTopic{Id= Guid.Parse("99182e8f-c816-4404-84d9-4e7402342ff9"),PromoterId=Guid.Parse("fb0f1566-cb66-48e4-8428-aa0f486ad579"), Available= ThesisTopicStatus.available, Topic="aplikacja do cwiczeń"},
            new ThesisTopic{Id= Guid.Parse("dd7edb11-9f57-408a-9f6f-d61fd6f52208"),PromoterId=Guid.Parse("7d89e79c-004c-417f-9320-ca4db26f633f"), Available= ThesisTopicStatus.available, Topic="moduł komunikacji satelitarnej"},
            new ThesisTopic{Id= Guid.Parse("4393d0d1-41ea-4349-8a15-6b5f347e5db7"),PromoterId=Guid.Parse("0df0bbd9-2303-40b1-8daf-a3ac7d3d02a8"), Available= ThesisTopicStatus.available, Topic="miernik elektroniczny"},
            new ThesisTopic{Id= Guid.Parse("540212d4-570a-48f1-86ae-20d8ecb95978"),PromoterId=Guid.Parse("4b0dd23c-b78e-436a-8971-bd82e1684d5d"), Available= ThesisTopicStatus.available, Topic="aplikacja zarządzania ruchem lotniczym"},
            new ThesisTopic{Id= Guid.Parse("25dc8625-629e-4f3d-ab47-5fb5d8f3e6fe"),PromoterId=Guid.Parse("42575545-8931-4f10-9fb6-7a17ef2915b9"), Available= ThesisTopicStatus.available, Topic="rozpoznawanie tablic rejetracyjnych" },
            new ThesisTopic{Id= Guid.Parse("08c0bc94-4888-4076-96fa-1c4edcd4c09e"),PromoterId=Guid.Parse("3a627291-c9fc-4e48-8ee8-e966edf27318"), Available= ThesisTopicStatus.available, Topic="rozpoznawanie znaków - sztuczna inteligencja" },
            new ThesisTopic{Id= Guid.Parse("9a1d0517-14fd-4c46-a884-dbf37aff098b"),PromoterId=Guid.Parse("5b6b3673-c6e1-4565-9d0b-65fedf905794"), Available= ThesisTopicStatus.available, Topic="Sklep internetowy" },
            new ThesisTopic{Id= Guid.Parse("826881cc-07e8-4fcb-8dce-a7b57ea2cebf"),PromoterId=Guid.Parse("734c295c-3ebe-478c-b129-7c95e108dc4c"), Available= ThesisTopicStatus.available, Topic="gra w szachy - apliakcja webowa"}
            };
            foreach (ThesisTopic e in topics)
            {
                context.ThesisTopics.Add(e);
            }
            context.SaveChanges();

            logger.LogInformation("Zakończono zasilanie bazy");
        }
    }
}
