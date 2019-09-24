using System;

namespace ENT0401___Tienda_control_compras_1._3
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Programa resuelto paso a paso:
            * 1. Preguntar número de productos a comprar.
            * 2. Preguntar nombre y precio (conservar el precio original) de cada producto.
            * 3. (Opcional) Sumar el precio total original.
            * 4. Hallar cuantos múltiplos de 3 hay para ver cuantos productos regalamos.
            * 5. Ordenar los productos por precio (de menor a mayor por ejemplo), para saber qué productos regalar (los de menor precio).
            * 6. Mostrar cada producto, el precio original y el precio que se aplica (solo cambia el precio de los productos regalados a: 0,00€)
            * 7. Mostrar el precio final a pagar de todos los productos.
            */
            int numproductos = 0;
            int productonum = 1;
            bool final = false;
            int mult3 = 0;
            decimal totalsindescuento = 0;
            decimal totaldescuento = 0;
            decimal totalfinal = 0;
            decimal burbujaprecio = 0;
            string burbujanombre = "";

            while (final == false)
            {
                Console.Write("Introduzca cuántos productos desea comprar: ");
                if (Int32.TryParse(Console.ReadLine(), out numproductos))		//Aceptación del número de productos (no se permiten 0 o números negativos)
                {
                    if (numproductos > 0)
                    {
                        Console.WriteLine("\n\nQuiere registrar la compra de {0} productos.", numproductos);
                        final = true;
                    }
                    else
                    {
                        Console.WriteLine("\n\nERROR: Los datos introducidos no son válidos. Ha introducido {0}. No se admiten números negativos ni 0. \nPor favor, vuelva a intentarlo.\n", numproductos);
                    }
                }
                else
                {
                    Console.WriteLine("\n\nERROR: Los datos introducidos no son válidos. Debe introducir un número. \nPor favor, vuelva a intentarlo.\n");
                }
            }
            final = false;      //Volvemos a poner el final como false para usar en otros bucles.

            mult3 = numproductos / 3;       //cuantos productos debemos descontar.
            if (mult3 != 0)
            {
                Console.WriteLine("\nYa que se comprarán {0} productos, se regalará/n {1} productos de menor precio.", numproductos, mult3);
            }
            else
            {
                Console.WriteLine("\nYa que se comprarán {0} productos, no se aplicará ningún descuento (se regala 1 producto por cada 3, los de menor precio de todos).", numproductos);
            }

            string[] nombreproducto = new string[numproductos];
            decimal[] precioproducto = new decimal[numproductos];   //declaramos los vectores para el nombre y el precio del producto con una longitud de índice del número de productos.

            for (int continventario = 0; continventario < numproductos; continventario++)       //Con este for rellenamos los vectores nombreproducto y precioproducto.
            {
                do
                {
                    Console.Write("\nIntroduzca el nombre del producto número {0}: ", productonum);		//Aceptación del nombre del producto
                    productonum++;
                    nombreproducto[continventario] = Console.ReadLine();
                    if (nombreproducto[continventario].Length != 0)
                    {
                        final = true;
                    }
                    else
                    {
                        productonum--;
                        Console.WriteLine("\n\nERROR: Los datos introducidos no son válidos. Ha dejado el nombre en blanco. \nPor favor, vuelva a intentarlo.\n");
                    }
                } while (final == false);
                final = false;

                do
                {
                    Console.Write("\nIntroduzca el precio (en euros) del producto {0}: ", nombreproducto[continventario]);		//Aceptación del precio con solo 2 decimales
                    if (Decimal.TryParse(Console.ReadLine(), out precioproducto[continventario]))
                    {
                        if ((precioproducto[continventario] * 100) % 1 == 0)
                        {
                            if (precioproducto[continventario] > 0)
                            {
                                Console.WriteLine("\nEl producto {0} tiene un precio de {1:f2} euros.", nombreproducto[continventario], precioproducto[continventario]);
                                totalsindescuento = totalsindescuento + precioproducto[continventario];
                                final = true;
                            }
                            else
                            {
                                Console.WriteLine("\n\nERROR: Los datos introducidos no son válidos. Ha introducido {0} euros. \nPor favor, vuelva a intentarlo.\n", precioproducto[continventario]);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\nERROR: Los datos introducidos no son válidos. Ha introducido ({0}) un precio con más de 2 decimales. \nPor favor, vuelva a intentarlo.\n", precioproducto[continventario]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\nERROR: Los datos introducidos no son válidos. Debe introducir un número. \nPor favor, vuelva a intentarlo.\n", precioproducto[continventario]);
                    }
                } while (final == false);
                final = false;
            }

            for (int contburbuja = 1; contburbuja < precioproducto.Length; contburbuja++)      //metodo burbuja para ordenar los productos según su precio de menor a mayor.
            {
                for (int contmenor = precioproducto.Length - 1; contmenor >= contburbuja; contmenor--)
                {
                    if (precioproducto[contmenor - 1] > precioproducto[contmenor])
                    {
                        burbujaprecio = precioproducto[contmenor - 1];
                        precioproducto[contmenor - 1] = precioproducto[contmenor];
                        precioproducto[contmenor] = burbujaprecio;
						
                        burbujanombre = nombreproducto[contmenor - 1];
                        nombreproducto[contmenor - 1] = nombreproducto[contmenor];
                        nombreproducto[contmenor] = burbujanombre;
                    }
                }
            }       //fin del método burbuja que ordena los valores almacenados en los vectores precioproducto y nombreproducto.

            decimal[] preciooriginal = new decimal[mult3];  //Declaramos le vector donde almacenar los precios originales antes de descuento.

            for (int contregalo = 0; contregalo < mult3; contregalo++)      //For que pone los productos que regalaremos a 0 euros, conservando su precio original.
            {
                totaldescuento = totaldescuento + precioproducto[contregalo];
                preciooriginal[contregalo] = precioproducto[contregalo];
                precioproducto[contregalo] = 0;
            }

            Console.WriteLine("\nFactura:");
            Console.WriteLine("__________________________________________________________________________________________");
            for (int contfactura = (numproductos - 1); contfactura >= 0; contfactura--)    //For para mostrar los productos.
            {
                if (precioproducto[contfactura] == 0)
                {
                    Console.WriteLine("\nProducto: {0}........{1:f2} euros", nombreproducto[contfactura], preciooriginal[contfactura]);
                    Console.WriteLine("\tDescuento por promoción: {0:f2} euros", preciooriginal[contfactura]);
                    Console.WriteLine("\t\tPrecio final producto: {0}........{1:f2} euros", nombreproducto[contfactura], precioproducto[contfactura]);
                }
                else
                {
                    Console.WriteLine("\nProducto: {0}........{1:f2} euros", nombreproducto[contfactura], precioproducto[contfactura]);     //Los productos que no han sido descontados conservan su precio.
                    Console.WriteLine("\t\tPrecio final producto: {0}........{1:f2} euros", nombreproducto[contfactura], precioproducto[contfactura]);    //Pero a petición del cliente, confirmamos que es su precio final.
                }
            }
            Console.WriteLine("___________________________________________________________________________________________");

            if (mult3 != 0)
            {
                Console.WriteLine("\nEl precio total de los productos antes del descuento es: {0:f2} euros", totalsindescuento);
                Console.WriteLine("El descuento total a aplicar es: {0:f2} euros", totaldescuento);
            }

            totalfinal = totalsindescuento - totaldescuento;
            Console.WriteLine("\nEl total final a pagar es: {0:f2} euros", totalfinal);

            Console.Write("\n\nPulse una tecla para finalizar...");
            Console.ReadKey();
        }
    }
}
