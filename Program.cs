using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    
    class Program
    {
        struct SinhVien
        {
            public int Maso;
            public string HoTen;
            public double DiemToan;
        }

         static void NhapThongTinSinhVien(SinhVien sv)
        {
            Console.Write("Ma so:");
            sv.Maso = int.Parse(Console.ReadLine());

        }
                
            static void XuatThongTinSinhVien(SinhVien sv)
            {
                Console.WriteLine("Ma so"+sv.Maso);
               

            }

            static void Main(string[] args)
        {

            SinhVien sv = new SinhVien();
            NhapThongTinSinhVien( sv);
            XuatThongTinSinhVien(sv);
            
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
