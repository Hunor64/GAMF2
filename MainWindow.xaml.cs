using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GAMF2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AFeladat();
            BFeladat();
            CFeladat();
            DFeladat();

        }
        public void AFeladat()
        {
            string[] sorok = File.ReadAllLines("telepules.txt");
            string[] megyek = File.ReadAllLines("megyek.txt");

            var megyekLakossag = new Dictionary<string, int>();
            foreach (var sor in sorok)
            {
                var adatok = sor.Split(' ');
                var megyeAzonosito = adatok[1];
                var lakossag = int.Parse(adatok[5]);

                if (!megyekLakossag.ContainsKey(megyeAzonosito))
                {
                    megyekLakossag[megyeAzonosito] = 0;
                }

                megyekLakossag[megyeAzonosito] += lakossag;
            }

            var rendezettMegyek = megyekLakossag.OrderBy(x => x.Value).ToList();
            var masodikLegkevesebb = rendezettMegyek[1];

            var megyeNeve = "";
            foreach (var sor in megyek)
            {
                var adatok = sor.Split(' ');
                if (adatok[0] == masodikLegkevesebb.Key)
                {
                    megyeNeve = adatok[1];
                    break;
                }
            }

            MessageBox.Show($"{megyeNeve}-{masodikLegkevesebb.Value}","A. Feladat");
        }
        public void BFeladat()
        {
            string[] sorok = File.ReadAllLines("telepules.txt");

            var legEszakibb = sorok.OrderByDescending(x => x.Split(' ')[2]).First();
            var legEszakibbTelepules = legEszakibb.Split(' ')[6];

            MessageBox.Show(legEszakibbTelepules,"B. Feladat");
        }
        public void CFeladat()
        {
            string[] sorok = File.ReadAllLines("telepules.txt");

            var kecskemetTavolsagok = sorok.Select(x => int.Parse(x.Split(' ')[7]));
            var szegedTavolsagok = sorok.Select(x => int.Parse(x.Split(' ')[8]));

            var ketKorbenBeleesok = kecskemetTavolsagok.Zip(szegedTavolsagok, (k, s) => k <= 50 && s <= 50).Count(x => x);

            MessageBox.Show(ketKorbenBeleesok.ToString(), "C. Feladat");

        }
        public void DFeladat()
        {
            string[] sorok = File.ReadAllLines("telepules.txt");

            var szurtSorok = sorok.Where(x => x.Split(' ')[2].StartsWith("47.3") || x.Split(' ')[2].StartsWith("47.4"));

            var elsoTelepules = szurtSorok.First();
            var elsoTerulet = float.Parse(elsoTelepules.Split(' ')[4]);

            var legnagyobbKulonbseg = 0.0;
            var elsoTelepulesNeve = elsoTelepules.Split(' ')[6];
            var masodikTelepulesNeve = "";

            foreach (var sor in szurtSorok.Skip(1))
            {
                var masodikTerulet = float.Parse(sor.Split(' ')[4]);
                var kulonbseg = Math.Abs(masodikTerulet - elsoTerulet);

                if (kulonbseg > legnagyobbKulonbseg)
                {
                    legnagyobbKulonbseg = kulonbseg;
                    masodikTelepulesNeve = sor.Split(' ')[6];
                }

                elsoTerulet = masodikTerulet;
            }

            MessageBox.Show($"{elsoTelepulesNeve}-{masodikTelepulesNeve}-{Math.Round(legnagyobbKulonbseg, 2)}","D. Feladat");

        }
        public void EFeladat()
        {

        }
        public void FFeladat()
        {

        }
    }
}
