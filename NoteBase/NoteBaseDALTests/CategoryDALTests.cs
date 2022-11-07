using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteBaseDAL;
using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using NoteBaseDALTests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseDAL.Tests
{
    [TestClass()]
    public class CategoryDALTests
    {
        //[ClassInitialize()]
        public static void CategoryDALTestsInitialize()
        {
            try
            {
                using (SqlConnection connection = new("Data Source=LAPTOP-AK9JEN2V;Initial Catalog=master;Integrated Security=True;Connect Timeout=300;"))
                {
                    FileInfo file = new(@"C:\Users\joeyj\Documents\DATA\school\Fontys\semester 2 electric boogaloo\individueel\Code\NoteBaseScripts\NoteBaseScripts\TestDBScript.sql");

                    string script = file.OpenText().ReadToEnd();

                    using (SqlCommand command = new(script, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        [TestMethod()]
        public void CreateTest_Succeed()
        {
            CategoryDALTestsInitialize();
            //arrange
            ICategoryDAL categoryDAL = Factory.CreateCategoryDAL();
            CategoryDTO category = new(0, "School", 1);
            CategoryDTO expectedCategory = new(12, "School", 1);

            DALResponse<CategoryDTO> expected = new(true);
            expected.AddItem(expectedCategory);

            //act
            DALResponse<CategoryDTO> actual = categoryDAL.Create(category);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);

            Assert.AreEqual(expected.Data[0].ID, actual.Data[0].ID);
            Assert.AreEqual(expected.Data[0].Title, actual.Data[0].Title);
            Assert.AreEqual(expected.Data[0].PersonId, actual.Data[0].PersonId);
        }

        [TestMethod()]
        public void CreateTest_Failed_TitleAreadyExists()
        {
            //arrange
            ICategoryDAL categoryDAL = Factory.CreateCategoryDAL();
            CategoryDTO category = new(0, "Games", 1);

            DALResponse<CategoryDTO> expected = new(false)
            {
                Message = "Category With this title already exists"
            };

            //act
            DALResponse<CategoryDTO> actual = categoryDAL.Create(category);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void CreateTest_Failed_TitleEmpty()
        {
            //arrange
            ICategoryDAL categoryDAL = Factory.CreateCategoryDAL();
            CategoryDTO category = new(0, "", 1);

            DALResponse<CategoryDTO> expected = new(false)
            {
                Message = "Title can't be empty"
            };

            //act
            DALResponse<CategoryDTO> actual = categoryDAL.Create(category);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        /*[TestMethod()]
        public void DeleteTest_Succeed()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            Response<Category> expected = new(true);

            //act
            Response<Category> actual = categoryProcessor.Delete(1);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
        }

        [TestMethod()]
        public void DeleteTest_Failed_NoteConnection()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            Response<Category> expected = new(false)
            {
                Message = "Notes exist with this category"
            };

            //act
            Response<Category> actual = categoryProcessor.Delete(2);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void DeleteTest_Failed_CategoryDoesNotExist()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            Response<Category> expected = new(false)
            {
                Message = "Category doesn't exist"
            };

            //act
            Response<Category> actual = categoryProcessor.Delete(999);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void UpdateTest_Succeed()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(1, "Games", 1);
            Category expectedCategory = new(1, "Games", 1);

            Response<Category> expected = new(true);
            expected.AddItem(expectedCategory);

            //act
            Response<Category> actual = categoryProcessor.Update(category);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);

            Assert.AreEqual(expected.Data[0].ID, actual.Data[0].ID);
            Assert.AreEqual(expected.Data[0].Title, actual.Data[0].Title);
            Assert.AreEqual(expected.Data[0].PersonId, actual.Data[0].PersonId);
        }

        [TestMethod()]
        public void UpdateTest_Failed_CategoryDoesNotExist()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(999, "Games", 1);

            Response<Category> expected = new(false)
            {
                Message = "Category doesn't exist"
            };

            //act
            Response<Category> actual = categoryProcessor.Update(category);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void UpdateTest_Failed_TitleEmpty()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(1, "", 1);

            Response<Category> expected = new(false)
            {
                Message = "Title can't be empty"
            };

            //act
            Response<Category> actual = categoryProcessor.Update(category);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        } */
    }
}