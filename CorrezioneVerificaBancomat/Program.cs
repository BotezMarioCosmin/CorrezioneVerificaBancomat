using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrezioneVerificaBancomat
{
    internal class Program
    {
        private ContoCorrente cc;
        private CartaBancomat cb;
        static void Main(string[] args)
        {
            ContoCorrente cc = new ContoCorrente("mrio25", "mario", "bancabergamo", 0);
            CartaBancomat cb = new CartaBancomat("mrio25_banc", "8219", cc);

            cb.preleva(2, "1231");
            Console.ReadKey();
        }
    }

    public class ContoCorrente
    {
        private string id; //primary key
        private string cliente;
        private string banca;
        private float saldo;

        public ContoCorrente(string id1)
        {

        }
        public ContoCorrente(string id1, string cliente1, string banca1, float saldo1)
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

        public void bonifico(ContoCorrente destinazione, float somma)
        {


        }
    }

    public class CartaBancomat
    {
        private string _id; //primary key
        private string _pin;
        private ContoCorrente cc;

        public CartaBancomat(string id1, string pin1, ContoCorrente cc)
        {
            setContoCorrente(cc);
            setId(id1);
            setPin(pin1);
        }

        private void setId(string id)
        {
            _id = id;
        }
        public string getId()
        {
            return _id;
        }
        private void setPin(string pin)
        {
            _pin = pin;
        }

        private ContoCorrente setContoCorrente(ContoCorrente conto)
        {
            return cc = conto;
        }

        public float getSaldo()
        {
            return cc.getSaldo();
        }

        public void deposita(float somma)
        {
            cc.deposita(somma);
        }

        public void preleva(float somma, string pin)
        {
            if (pin == _pin)
            {
                cc.preleva(somma);
            }
            else
            {
                Console.WriteLine("Pin errato");
            }
        }
    }
}

