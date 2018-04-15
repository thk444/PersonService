using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonServiceLibrary;
using System.Collections.Generic;

namespace PersonServiceTests
{
    [TestClass]
    public class PersonServiceTest
    {
        [TestMethod]
        [TestCategory("Record Parsing")]
        [ExpectedException(typeof(FormatException))]
        public void ParsePersonRecord_EmptyRecord()
        {
            PersonService PS = PersonService.GetInstance;

            string record = "";

            PS.ParsePersonRecord(record);
        }

        [TestMethod]
        [TestCategory("Record Parsing")]
        [ExpectedException(typeof(FormatException))]
        public void ParsePersonRecord_Recordwithlesscolumns()
        {
            PersonService PS = PersonService.GetInstance;

            string record = "Talapaneni,Hemanth,Male,2017/12/01";

            PS.ParsePersonRecord(record);
        }

        [TestMethod]
        [TestCategory("Record Parsing")]
        [ExpectedException(typeof(FormatException))]
        public void ParsePersonRecord_Recordwithmorecolumns()
        {
            PersonService PS = PersonService.GetInstance;

            string record = "Talapaneni,Hemanth,Male,Yellow,Green,2017/12/01";

            PS.ParsePersonRecord(record);
        }

        [TestMethod]
        [TestCategory("Record Parsing")]
        [ExpectedException(typeof(FormatException))]
        public void ParsePersonRecord_RecordwithinvalidDelimiter()
        {
            PersonService PS = PersonService.GetInstance;

            string record = "Talapaneni:Hemanth:Male:Yellow:2017/12/01";

            PS.ParsePersonRecord(record);
        }

        [TestMethod]
        [TestCategory("Record Parsing")]
        [ExpectedException(typeof(FormatException))]
        public void ParsePersonRecord_RecordwithmixedDelimiter()
        {
            PersonService PS = PersonService.GetInstance;
            string record = "Talapaneni,Hemanth|Male,Yellow,2017/12/01";

            PS.ParsePersonRecord(record);
        }

        [TestMethod]
        [TestCategory("Record Parsing")]
        public void ParsePersonRecord_GoodRecordwithcommaDelimiter()
        {
            PersonService PS = PersonService.GetInstance;
            string record = "Talapaneni,Hemanth,Male,Yellow,2017/12/01";

            PS.ParsePersonRecord(record);

            if (PS.PersonRepository[0] != null)
            {
                Person P = PS.PersonRepository[0];
                Assert.AreEqual("Talapaneni", P.LastName, true);
            }
            else
            {
                Assert.AreEqual(1, 2);
            }
        }

        [TestMethod]
        [TestCategory("Record Parsing")]
        public void ParsePersonRecord_GoodRecordwithpipeDelimiter()
        {
            PersonService PS = PersonService.GetInstance;

            string record = "Talapaneni|Hemanth|Male|Yellow|2017/12/01";

            PS.ParsePersonRecord(record);

            if (PS.PersonRepository[0] != null)
            {
                Person P = PS.PersonRepository[0];
                Assert.AreEqual("Talapaneni", P.LastName, true);
            }
            else
            {
                Assert.AreEqual(1, 2);
            }
        }

        [TestMethod]
        [TestCategory("Record Parsing")]
        public void ParsePersonRecord_GoodRecordwithspaceDelimiter()
        {
            PersonService PS = PersonService.GetInstance;
            string record = "Talapaneni Hemanth Male Yellow 2017/12/01";

            PS.ParsePersonRecord(record);

            if (PS.PersonRepository[0] != null)
            {
                Person P = PS.PersonRepository[0];
                Assert.AreEqual("Talapaneni", P.LastName, true);
            }
            else
            {
                Assert.AreEqual(1, 2);
            }
        }

        [TestMethod]
        [TestCategory("Record Save")]
        [ExpectedException(typeof(FormatException))]
        public void SavePerson_InvalidDate()
        {
            PersonService PS = PersonService.GetInstance;
            string record = "Talapaneni Hemanth Male Yellow 2017/13/01";

            PS.ParsePersonRecord(record);

            if (PS.PersonRepository[0] != null)
            {
                Person P = PS.PersonRepository[0];
                Assert.AreEqual("Talapaneni", P.LastName, true);
            }
            else
            {
                Assert.AreEqual(1, 2);
            }
        }

        [TestMethod]
        [TestCategory("Record Save")]
        [ExpectedException(typeof(FormatException))]
        public void SavePerson_StringForDate()
        {
            PersonService PS = PersonService.GetInstance;
            string record = "Talapaneni Hemanth Male Yellow December";

            PS.ParsePersonRecord(record);

            if (PS.PersonRepository[0] != null)
            {
                Person P = PS.PersonRepository[0];
                Assert.AreEqual("Talapaneni", P.LastName, true);
            }
            else
            {
                Assert.AreEqual(1, 2);
            }
        }

        [TestMethod]
        [TestCategory("Record Sort")]
        public void GetPersons_orderbyGender_Test()
        {
            PersonService PS = PersonService.GetInstance;
            PS.PersonRepository.Clear();
            PS.ParsePersonRecord("Talapaneni Hemanth Male Yellow 2017/12/01");
            PS.ParsePersonRecord("Halapaneni Temanth Female Green 2018/2/1");
            PS.ParsePersonRecord("Balapaneni Remanth Female Blue 2014/5/01");
            PS.ParsePersonRecord("Malapaneni Bemanth Male Red 2015/11/1");

            List<Person> persons = PS.GetPersons_orderbyGender();

            Assert.AreEqual("Halapaneni", persons[0].LastName);
            Assert.AreEqual("Balapaneni", persons[1].LastName);
            Assert.AreEqual("Talapaneni", persons[2].LastName);
            Assert.AreEqual("Malapaneni", persons[3].LastName);
        }

        [TestMethod]
        [TestCategory("Record Sort")]
        public void GetPersons_orderbyLastNameDescending_Test()
        {
            PersonService PS = PersonService.GetInstance;
            PS.PersonRepository.Clear();
            PS.ParsePersonRecord("Talapaneni Hemanth Male Yellow 2017/12/01");
            PS.ParsePersonRecord("Halapaneni Temanth Female Green 2018/2/1");
            PS.ParsePersonRecord("Balapaneni Remanth Female Blue 2014/5/01");
            PS.ParsePersonRecord("Malapaneni Bemanth Male Red 2015/11/1");

            List<Person> persons = PS.GetPersons_orderbyLastNameDescending();

            Assert.AreEqual("Talapaneni", persons[0].LastName);
            Assert.AreEqual("Malapaneni", persons[1].LastName);
            Assert.AreEqual("Halapaneni", persons[2].LastName);
            Assert.AreEqual("Balapaneni", persons[3].LastName);
        }

        [TestMethod]
        [TestCategory("Record Sort")]
        public void GetPersons_orderbyBirthDate_Test()
        {
            PersonService PS = PersonService.GetInstance;
            PS.PersonRepository.Clear();
            PS.ParsePersonRecord("Talapaneni Hemanth Male Yellow 2017/12/01");
            PS.ParsePersonRecord("Halapaneni Temanth Female Green 2018/2/1");
            PS.ParsePersonRecord("Balapaneni Remanth Female Blue 2014/5/01");
            PS.ParsePersonRecord("Malapaneni Bemanth Male Red 2015/11/1");

            List<Person> persons = PS.GetPersons_orderbyBirthDate();

            Assert.AreEqual("Balapaneni", persons[0].LastName);
            Assert.AreEqual("Malapaneni", persons[1].LastName);
            Assert.AreEqual("Talapaneni", persons[2].LastName);
            Assert.AreEqual("Halapaneni", persons[3].LastName);
        }
    }
}
