using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CorrezioneVerificaBancomat
{
    internal class Program
    {
        private ContoCorrente cc;
        private CartaBancomat cb;
        private SportelloBancomat sp;
        static void Main(string[] args)
        {
            ContoCorrente cc1 = new ContoCorrente("mrio25", "mario", "bancabergamo", 0);
            ContoCorrente cc2 = new ContoCorrente("gvn25", "giovanni", "bancabergamo", 0);
            CartaBancomat cb1 = new CartaBancomat("mrio25_banc", "8219", cc1);
            CartaBancomat cb2 = new CartaBancomat("gvn_banc", "9271", cc2);
            SportelloBancomat sp = new SportelloBancomat("sprtl_01", "via roma", "bancaitalia");

            cc1.deposita(100);
            try
            {
                cc1.preleva(50);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Saldo: " + cc1.getSaldo());

            cc2.deposita(1000);
            try
            {
                cc2.preleva(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Saldo: " + cc2.getSaldo());

            cb1.deposita(40);

            try
            {
                cb2.preleva(50, "9271");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }

    public class ContoCorrente
    {
        private string _id; //primary key
        private string _cliente;
        private string _banca;
        private float _saldo;

        public ContoCorrente(string id1)
        {
            setId(id1);
            setCliente(id1);
            setBanca("x");
            setSaldo(0);
        }
        public ContoCorrente(string id1, string cliente1, string banca1, float saldo1)
        {
            setId(id1);
            setCliente(cliente1);
            setBanca(banca1);
            setSaldo(saldo1);
        }

        public string getId()
        {
            return _id;
        }
        private void setId(string id)
        {
            _id = id;
        }

        public string getBanca()
        {
            return _banca;
        }
        private void setBanca(string banca)
        {
            _banca = banca;
        }

        public string getCliente()
        {
            return _cliente;
        }
        private void setCliente(string cliente)
        {
            _cliente = cliente;
        }

        public float getSaldo()
        {
            return _saldo;
        }
        private void setSaldo(float saldo)
        {
            _saldo = saldo;
        }

        public void deposita(float somma)
        {
            if (somma > 0)
            {
                _saldo += somma;
            }
            else
                throw new Exception("Somma non valida");
        }
        public void preleva(float somma)
        {
            if (somma > 0 && getSaldo() >= somma)
            {
                _saldo -= somma;
            }
            else
                throw new Exception("Saldo insufficiente");
        }

        public void bonifico(ContoCorrente destinazione, float somma)
        {
            if (_saldo >= somma)
            {
                preleva(somma);
                destinazione.deposita(somma);
            }
            else
                throw new Exception("Saldo insufficiente");

        }
    }

    public class CartaBancomat
    {
        private string _id; //primary key
        private string _pin;
        private ContoCorrente cc;
        public bool bloccata = false;

        public CartaBancomat(string id1, string pin1, ContoCorrente cc)
        {
            setContoCorrente(cc);
            setId(id1);
            setPin(pin1);
        }

        public void setBloccata(bool b)
        { 
            bloccata = b;
        }
        public bool getBloccata()
        { 
            return bloccata;
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
                throw new Exception("Pin errato");
            }
        }
    }

    public class SportelloBancomat
    {
        private string _id; //primary key
        private string _indirizzo;
        private string _banca;
        private CartaBancomat cb;
        public string[] arrayIdBloccati;
        public bool stato;

        public SportelloBancomat(string id1, string indirizzo1, string banca1)
        {
            setId(id1);
            setIndirizzo(indirizzo1);
            setBanca(banca1);
        }

        public SportelloBancomat(string id1, string indirizzo1, string banca1, string[] array)
        {
            setId(id1);
            setIndirizzo(indirizzo1);
            setBanca(banca1);
            setArrayBloc(array);
        }

        private void setArrayBloc(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                arrayIdBloccati[i] = array[i];
            }
        }
        private CartaBancomat setCartaBancomat(CartaBancomat carta)
        {
            return cb = carta;
        }

        private void setId(string id)
        {
            _id = id;
        }
        public string getId()
        {
            return _id;
        }
        private void setIndirizzo(string ind)
        {
            _indirizzo = ind;
        }
        public string getIndirizzo()
        {
            return _indirizzo;
        }
        private void setBanca(string banca)
        {
            _banca = banca;
        }
        public string getBanca()
        {
            return _banca;
        }
        ///da qua in poi dubbi
        public void inserisci(CartaBancomat cb)
        {
            stato = false;
            for (int i = 0; i < arrayIdBloccati.Length; i++)
            {
                if (cb.getId() == arrayIdBloccati[i])
                {
                    cb.setBloccata(true);
                }
                else
                    cb.setBloccata(false);
            }
        }

        public CartaBancomat rimuovi()
        {
            if (cb.getBloccata() == false)
            {
                return getCarta();
            }
            else
            {
                return null;
            }
        }

        public CartaBancomat getCarta()
        {
            return cb;
        }
    }
}

