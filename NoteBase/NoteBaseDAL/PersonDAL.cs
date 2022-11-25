using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseDAL
{
    public class PersonDAL : IPersonDAL
    {
        private readonly string ConnString;

        public PersonDAL(string _connString)
        {
            ConnString = _connString;
        }

        public DALResponse<PersonDTO> Create(PersonDTO _person)
        {
            DALResponse<PersonDTO> response = new(true);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"INSERT INTO Person (Name, Email) VALUES (@Name, @Email)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", _person.Name);
                        command.Parameters.AddWithValue("@Email", _person.Email);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            int result = reader.GetInt32(0);
                            if (result == 0)
                            {
                                response.Succeeded = false;
                                response.Message = "PersonDAL.Create(" + _person.ID + ") ERROR: Could not Create Person";
                            }
                        }
                    }
                }
            }
            //het opvangen van een mogelijke error
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "PersonDAL.Create(" + _person.ID + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "PersonDAL.Create(" + _person.ID + ") ERROR: " + e.Message
                };
            }

            return response;
        }

        public DALResponse<PersonDTO> GetByEmail(string _personEmail)
        {
            DALResponse<PersonDTO> response = new(true);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Name, Email FROM Person WHERE Email = @Email";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", _personEmail);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            PersonDTO personDTO = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));

                            response.AddItem(personDTO);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "PersonDAL.GetByEmail(" + _personEmail + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "PersonDAL.GetByEmail(" + _personEmail + ") ERROR: " + e.Message
                };
            }

            return response;
        }

        public DALResponse<PersonDTO> Update(PersonDTO _person)
        {
            throw new NotImplementedException();
        }

        public DALResponse<PersonDTO> Delete(int _personId)
        {
            throw new NotImplementedException();
        }
    }
}
