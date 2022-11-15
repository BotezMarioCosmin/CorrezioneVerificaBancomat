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
            ContoCorrente cc = new ContoCorrente("mrio25", "mario", "bancabergamo", 0);
            CartaBancomat cb = new CartaBancomat("mrio25_banc", "8219", cc);
            SportelloBancomat sp = new SportelloBancomat("sprtl_01", "via roma", "bancaitalia");

            //cb.preleva(2, "1231");
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
                Console.WriteLine("Somma non valida");
        }
        public void preleva(float somma)
        {
            if (somma > 0 && getSaldo() >= somma)
            {
                _saldo -= somma;
            }
            else
                Console.WriteLine("Saldo insufficiente");
        }

        public void bonifico(ContoCorrente destinazione, float somma)
        {
            if (_saldo >= somma)
            {
                preleva(somma);
                destinazione.deposita(somma);
            }
            else
                Console.WriteLine("Saldo insufficiente");

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
                Console.WriteLine("Pin errato");
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

