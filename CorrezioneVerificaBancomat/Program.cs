using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrezioneVerificaBancomat
{
    internal class Program
    {
        private SportelloBancomat sp;
        static void Main(string[] args)
        {
            SportelloBancomat sp = new SportelloBancomat("mrio25", "mario", "bancabergamo", 0);


        }
    }

    public class SportelloBancomat
    {
        private string id; //primary key
        private string cliente;
        private string banca;
        private float saldo;

        public SportelloBancomat(string id1)
        {

        }
        public SportelloBancomat(string id1, string cliente1, string banca1, float saldo1)
        {

        }

        public string getId()
        {
            return id;
        }

        public string getBanca()
        {
            return banca;
        }

        public string getCliente()
        {
            return cliente;
        }

        public float getSaldo()
        {
            return saldo;
        }

        public void deposita(float somma)
        {
            if (somma > 0)
            {
                saldo += somma;
            }
            else
                Console.WriteLine("Somma non valida");
        }
        public void preleva(float somma)
        {
            if (somma > 0 && getSaldo() >= somma)
            {
                saldo -= somma;
            }
            else
                Console.WriteLine("Saldo insufficiente");
        }
    }

    public class CartaBancomat
    {
        private string _id; //primary key
        private string _pin;
        private SportelloBancomat sp;

        public CartaBancomat(string id1, string pin1, SportelloBancomat sp1)
        {
            _id = id1;
            _pin = pin1;
        }

        public string getId()
        {
            return _id;
        }

        public float getSaldo()
        {
            return sp.getSaldo();
        }

        public void deposita(float somma)
        {
            sp.deposita(somma);
        }

        public void preleva(float somma, string pin)
        {
            if (pin == _pin)
            {
                sp.preleva(somma);
            }
            else
            {
                Console.WriteLine("Pin errato");
            }
        }
    }
}

