using System;
using System.Collections.Generic;

namespace SistemKaryawan
{
    public class Karyawan
    {
        public string Nama { get; set; }
        public double Gaji { get; set; }

        public Karyawan(string nama, double gaji)
        {
            Nama = nama;
            Gaji = gaji;
        }

        public virtual void Kerja()
        {
            Console.WriteLine($"{Nama} sedang bekerja.");
        }

        public virtual void InfoKaryawan()
        {
            Console.WriteLine($"Nama: {Nama}, Gaji: Rp{Gaji:N0}");
        }
    }

    public class Tetap : Karyawan
    {
        public double Tunjangan { get; set; }

        public Tetap(string nama, double gaji, double tunjangan) : base(nama, gaji)
        {
            Tunjangan = tunjangan;
        }

        public double HitungGajiTotal()
        {
            return Gaji + Tunjangan;
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} (Karyawan Tetap) bekerja sesuai jam kerja kantor.");
        }

        public override void InfoKaryawan()
        {
            base.InfoKaryawan();
            Console.WriteLine($"Tunjangan: Rp{Tunjangan:N0}, Gaji Total: Rp{HitungGajiTotal():N0}");
        }
    }

    public class Manager : Tetap
    {
        public Manager(string nama, double gaji, double tunjangan) : base(nama, gaji, tunjangan) { }

        public void Memimpin()
        {
            Console.WriteLine($"{Nama} sedang memimpin rapat tim.");
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} (Manager) mengatur strategi dan mengawasi tim.");
        }
    }

    public class Staff : Tetap
    {
        public Staff(string nama, double gaji, double tunjangan) : base(nama, gaji, tunjangan) { }

        public void KerjakanTugas()
        {
            Console.WriteLine($"{Nama} sedang mengerjakan tugas harian.");
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} (Staff) mengerjakan tugas operasional.");
        }
    }

    public class Kontrak : Karyawan
    {
        public int Durasi { get; set; } // dalam bulan

        public Kontrak(string nama, double gaji, int durasi) : base(nama, gaji)
        {
            Durasi = durasi;
        }

        public void CekKontrak()
        {
            Console.WriteLine($"Kontrak {Nama} berlaku selama {Durasi} bulan.");
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} (Kontrak) bekerja sesuai masa kontrak.");
        }

        public override void InfoKaryawan()
        {
            base.InfoKaryawan();
            Console.WriteLine($"Durasi Kontrak: {Durasi} bulan");
        }
    }

    public class Magang : Kontrak
    {
        public Magang(string nama, double gaji, int durasi) : base(nama, gaji, durasi) { }

        public void Belajar()
        {
            Console.WriteLine($"{Nama} sedang belajar dari mentor.");
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} (Magang) membantu pekerjaan sambil belajar.");
        }
    }

    public class Freelancer : Kontrak
    {
        public Freelancer(string nama, double gaji, int durasi) : base(nama, gaji, durasi) { }

        public void AmbilProyek()
        {
            Console.WriteLine($"{Nama} mengambil proyek baru.");
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} (Freelancer) mengerjakan proyek secara mandiri.");
        }
    }

    public class Perusahaan
    {
        private List<Karyawan> daftarKaryawan = new List<Karyawan>();

        public void TambahKaryawan(Karyawan karyawan)
        {
            daftarKaryawan.Add(karyawan);
            Console.WriteLine($"{karyawan.Nama} ditambahkan ke perusahaan.");
        }

        public void DaftarKaryawan()
        {
            int no = 1;
            foreach (Karyawan k in daftarKaryawan)
            {
                Console.WriteLine($"--- Karyawan #{no++} ({k.GetType().Name}) ---");
                k.InfoKaryawan();
            }
        }

        public void KerjakanSemua()
        {
            foreach (Karyawan k in daftarKaryawan)
            {
                k.Kerja(); // polymorphism
            }
        }

        public List<Karyawan> Semua => daftarKaryawan;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Perusahaan perusahaan = new Perusahaan();

            Manager manager = new Manager("Budi", 15000000, 5000000);
            Staff staff = new Staff("Sari", 8000000, 2000000);
            Magang magang = new Magang("Dani", 2000000, 6);
            Freelancer freelancer = new Freelancer("Rina", 6000000, 3);

            Console.WriteLine("=== MENAMBAH KARYAWAN ===");
            perusahaan.TambahKaryawan(manager);
            perusahaan.TambahKaryawan(staff);
            perusahaan.TambahKaryawan(magang);
            perusahaan.TambahKaryawan(freelancer);

            Console.WriteLine("\n=== DAFTAR KARYAWAN ===");
            perusahaan.DaftarKaryawan();

            Console.WriteLine("\n=== POLYMORPHISM: Kerja() SEMUA KARYAWAN ===");
            perusahaan.KerjakanSemua();
            
            Console.WriteLine("\n=== METHOD KHUSUS ===");
            manager.Memimpin();
            staff.KerjakanTugas();
            magang.Belajar();
            freelancer.AmbilProyek();
            magang.CekKontrak();
            Console.WriteLine($"Gaji total {manager.Nama}: Rp{manager.HitungGajiTotal():N0}");

            Console.WriteLine("\n=== SOAL 1: Kerja() pada Manager dan Freelancer ===");
            manager.Kerja();
            freelancer.Kerja();

            Console.WriteLine("\n=== SOAL 2: Memimpin() pada Manager ===");
            manager.Memimpin();

            Console.WriteLine("\n=== SOAL 3: Info lengkap Manager ===");
            manager.InfoKaryawan();

            Console.WriteLine("\n=== SOAL 4: Belajar() pada Magang ===");
            magang.Belajar();

            Console.WriteLine("\n=== SOAL 5: Variabel Karyawan berisi Staff ===");
            Karyawan k = new Staff("Andi", 7000000, 1500000);
            k.Kerja();
            
            ((Staff)k).KerjakanTugas(); // bisa lewat casting

            Console.ReadKey();
        }
    }
}