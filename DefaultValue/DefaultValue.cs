using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.DefaultValue
{
    public static class DefaultValue
    {
        public static void InsertDefaultValues(ApplicationDBContext _context)
        {
             try
            {
                //create first default user
                User? dbUser = _context.Users.FirstOrDefault(user => user.Role == Role.Admin);
                if (dbUser is null)
                {
                    if (_context.Cities.ToList().Count == 0)
                    {


                        List<string> algerianCities = new List<string>
                        {
                            "Adrar",
                            "Chlef",
                            "Laghouat",
                            "Oum_El_Bouaghi",
                            "Batna",
                            "Béjaïa",
                            "Biskra",
                            "Bechar",
                            "Blida",
                            "Bouira",
                            "Tamanrasset",
                            "Tébessa",
                            "Tlemcen",
                            "Tiaret",
                            "Tizi_Ouzou",
                            "Alger",
                            "Djelfa",
                            "Jijel",
                            "Sétif",
                            "Saïda",
                            "Skikda",
                            "Sidi_Bel_Abbès",
                            "Annaba",
                            "Guelma",
                            "Constantine",
                            "Médéa",
                            "Mostaganem",
                            "Msila",
                            "Mascara",
                            "Ouargla",
                            "Oran",
                            "El_Bayadh",
                            "Illizi",
                            "Bordj_Bou_Arreridj",
                            "Boumerdès",
                            "El_Tarf",
                            "Tindouf",
                            "Tissemsilt",
                            "El_Oued",
                            "Khenchela",
                            "Souk_Ahras",
                            "Tipaza",
                            "Mila",
                            "Aïn_Defla",
                            "Naâma",
                            "Aïn_Témouchent",
                            "Ghardaïa",
                            "Relizane",
                            "Timimoun",
                            "Bordj_Baji_Mokhtar",
                            "Béni_Abbès",
                            "Ouled_Djellal",
                            "In_Salah",
                            "In_Guezzam",
                            "Touggourt",
                            "Djanet",
                            "El_Mghair",
                            "El_Meniaa"
                        };

                        // Print out the list of cities
                        foreach (string city in algerianCities)
                        {
                            _context.Cities.Add(new City
                            {
                                Name = city,
                                SectorID = null
                            });
                        }
                    }

                    _context.Users.Add(new User
                    {
                        UserName = "Admin",
                        Password = BCrypt.Net.BCrypt.HashPassword("Admin"),
                        FirstName = "Admin",
                        LastName = "Admin",
                        CityID = 23,
                        Address = "Admin",
                        PhoneNumber = "0123456789",
                        Email = "Admin@mail.com",
                        Role = Models.Enums.Role.Admin,
                    });

                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}