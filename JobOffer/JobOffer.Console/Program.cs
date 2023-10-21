using JobOffer.Domain.Models;
using JobOffer.infrastructure;
using JobOffer.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace JobOfferConsola
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.Title = "login";
            menu.writelogo();
            Console.WriteLine("bem-vindo");
            menu.menuprincipal();

            //using (var db = new JobOfferDBcontext())
            //{
            //    Console.WriteLine($"-Database path: {db.DbPath}");
            //}





        }
    }
}

