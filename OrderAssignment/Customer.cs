using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace OrderAssignment
{
    internal class Customer
    {
        private static readonly string _connectionString = @"Data Source=DESKTOP-AMR2CQS\MSSQLSERVER01;Initial Catalog=OrderAssignment;Integrated Security=True";
        private readonly SqlConnection _connection = new SqlConnection(_connectionString);
        private SqlDataAdapter sqlDataAdapter;
        private DataTable getDataItem = new DataTable();
        private string FirstName;
        private string LastName;
        private long? PhoneNumber;
        private string Email;
        private bool CustomerExistOrNot(string mail)
        {
            DataTable dt = new DataTable(); 
            string sql = "select * from Customers where Email='" + mail + "'";
            sqlDataAdapter = new SqlDataAdapter(sql, _connection);
            sqlDataAdapter.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        public void AddCustomers()
        {
            addmore:
            try
            {
                try
                {
                    Console.WriteLine("Enter the First Name");
                    FirstName = Console.ReadLine();
                    Console.WriteLine("Enter The Last Name");
                    LastName = Console.ReadLine();
                    Console.WriteLine("Enter the Phone Number It Should be Cantain Only 10 Digits Number no more no less");
                    PhoneNumber = Convert.ToInt64(Console.ReadLine());
                EnterValidmail:
                    Console.WriteLine("Enter the Email Address");

                    Email = Console.ReadLine();
                    if (!validmail(Email))
                    {
                        Console.WriteLine("Enter Valid Email Address => " + "it is not valid " + Email);
                        goto EnterValidmail;
                    }
                }

                catch (FormatException ex)
                {
                    Console.WriteLine();
                    goto addmore;
                }
                if(FirstName !="" && LastName !="" && PhoneNumber.ToString().Length==10 && Email != "")
                {                   
                    
                        if (!CustomerExistOrNot(Email)&& validmail(Email))
                        {
                            string sql = "insert into Customers values('" + FirstName + "','" + LastName + "'," + PhoneNumber + ",'" + Email + "')";
                            sqlDataAdapter = new SqlDataAdapter(sql, _connection);
                            sqlDataAdapter.Fill(getDataItem);
                            Console.WriteLine("Customer Add Successfully ");
                           // emailsend(Email,FirstName,LastName);
                            Console.WriteLine();
                            Console.WriteLine("Do you want to Add More Customers \n             press :1");
                            int check = Convert.ToInt32(Console.ReadLine());
                            if (check == 1)
                            {
                            goto addmore;
                            }

                        }
                        else
                        {
                            Console.WriteLine("User Already Exist Use Another   ");
                            goto addmore;
                        }
                    

                


            }
                else
                {
                    Console.WriteLine(" You Missed Some Credential constraints");
                    Console.WriteLine("try Again");
                    goto addmore;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
        public void DeleteCustomers()
        {
            deleteAgain:
            Console.WriteLine("Enter The Valid Eamil That Customer You Want to delete");
            Email=Console.ReadLine();
            if (validmail(Email))
            {
                if (CustomerExistOrNot(Email))
                {
                    string sql = "delete from Customers where Email='" + Email + "'";
                    sqlDataAdapter= new SqlDataAdapter(sql, _connection);
                    sqlDataAdapter.Fill(getDataItem);
                    Console.WriteLine("Customer Delete Successfully..");
                    Console.WriteLine();
                    Console.WriteLine("Do You Wnat To Delete More \n              press :1");
                    int check=int.Parse(Console.ReadLine());    
                    if(check == 1)
                    {
                        goto deleteAgain;
                    }

                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Email Does not exist in the record");
                    goto deleteAgain;

                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Enter the Valid Email Address THis Email Format Not Correct => "+Email);
                goto deleteAgain;

            }
        }
        public void UpdateCustomers()
        {
            string newEmail;
        updateAgain:
            try
            {
                Console.WriteLine("Enter the Email That User you Want to Update");
                Email = Console.ReadLine();
                if (!validmail(Email))
                {
                    Console.WriteLine("Please Enter The Valid Email Adress");
                    goto updateAgain;
                }
                else
                {
                    if (CustomerExistOrNot(Email))
                    {
                        updateAgaindata:
                        try
                        {
                            Console.WriteLine("Enter The First Name");
                            FirstName = Console.ReadLine();
                            Console.WriteLine("Enter the Last Nmae");
                            LastName = Console.ReadLine();
                            Console.WriteLine("Enter The Phone Number");
                            PhoneNumber = Convert.ToInt64(Console.ReadLine());
                        validmailAddress:
                            Console.WriteLine("Enter New Email");
                            newEmail = Console.ReadLine();
                            if (!validmail(newEmail))
                            {
                                Console.WriteLine("Email Address Is Not Valid  Put Valid Email");
                                goto validmailAddress;

                            }
                         
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message + "\nSome Information format Incorrect try Again");
                            goto updateAgaindata;
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            goto updateAgaindata;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Customer Does not Exist with This Email Address  "+Email );
                        Console.WriteLine("Enter Again");
                        goto updateAgain;
                    }
                    if (FirstName != "" && LastName != "" && PhoneNumber.ToString().Length == 10 && CustomerExistOrNot(Email))
                    {
                        string sql = "update Customers set FirstName='" + FirstName + "',LastName='" + LastName + "',Phone_no=" + PhoneNumber + ",Email='" + newEmail + "' where Email='" + Email + "'";
                        sqlDataAdapter = new SqlDataAdapter(sql, _connection);
                        sqlDataAdapter.Fill(getDataItem);
                        Console.WriteLine(" customers Update Successfully");

                        Console.WriteLine();
                        Console.WriteLine(" do you Want To UpDate More Customers\n                           press :1");
                        int check = int.Parse(Console.ReadLine());
                        if (check == 1)
                        {
                            goto updateAgain;
                        }
                    }
                    else
                    {
                        Console.WriteLine("  You Enter Some Detail Incorrect...");
                        Console.WriteLine(" Put Correct ForMat....");
                        goto updateAgain;
                    }

                }

            }
            catch(Exception ex)
            {

            }
        }
        public void ListallCustomer()
        {
            DataTable dt = new DataTable();
            try
            {
                sqlDataAdapter = new SqlDataAdapter("select * from Customers", _connection);
                sqlDataAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //print column
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        Console.Write(dt.Columns[i].ColumnName + " |    ");

                    }
                    Console.WriteLine();

                    //print data
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            Console.Write(dt.Rows[i][j] + "           ");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No Data Found...");
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
              

        }
        private void  CustomerMemu()
        {
            Console.WriteLine();
            Console.WriteLine("*****************************************************************");
            Console.WriteLine("Add Customer      Press :1                                      *");
            Console.WriteLine("Delete Customer   Press :2                                      *");
            Console.WriteLine("update Customer   Press :3                                      *");
            Console.WriteLine("Show All Customer Press :4                                      *");
            Console.WriteLine("Exit              Press :5                                      *");
            Console.WriteLine("*****************************************************************");
            Console.WriteLine();
        }
        public void emailsend(string to,string firstname,string lastname)
        {
            string from = "sinhgsanjay790043@gmail.com";
            long password = 7900434644;
            string subject = "Welcome  Dear Customre";
            string body = "<h1>Dear, "+firstname+" "+LastName+ "</h1>\nThanks for registering with us";
            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(from),
                    Subject = subject,
                    Body = body
                };
                mail.To.Add(new MailAddress(to));
                mail.IsBodyHtml = true;



                SmtpClient smpt = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    // smpt.Timeout = 10000;
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    EnableSsl = true,

                    Credentials = new NetworkCredential(from, password.ToString())
                };
                smpt.Send(mail);





            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        public bool validmail(string email)
        {
            Regex eml = new Regex(@"^[a-zA-Z]+[._]{0,1}[0-9a-zA-Z]+[@][a-zA-Z]+[.][a-zA-Z]{2,3}([.]+[a-zA-Z]{2,3}){0,1}");
            Match m = eml.Match(email);
            if (m.Success)
            {
                return true;

            }
            else
            {
                return false;
            }

        }
        public void portal()
        {
        portalAgain:
            CustomerMemu();
            int choise=Convert.ToInt32(Console.ReadLine());
            switch(choise)
            {
                case 1:
                    AddCustomers();
                    goto portalAgain;
                case 2:
                    DeleteCustomers();
                    goto portalAgain;
                case 3:
                    UpdateCustomers();
                    goto portalAgain;
                case 4:
                    ListallCustomer();
                    goto portalAgain;
                case 5:
                    break;


                default:
                    Console.WriteLine(" Wrong Choise");
                    goto portalAgain;   
            }


        }

    }
}
