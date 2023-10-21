using JobOffer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobOffer.Domain.SeekWork
{
    public interface IRepository<T> where T : Entity
    {
        void Create(T entity);// criar qualuqer tipo de resgitro da base de dados
        void Update(T entity);// update qualuqer tipo de resgitro da base de dados
        void Delete(T entity);// deletar qualuqer tipo de resgitro da base de dados
        T FindbyId(int id); // encontrar na base de dados a partir do Id T é igual a um valor generico(não sabemos o valor)
        void Save();// salvar


        List<T> FindAll();// encontrar a lista a partir de uma entity
       
    }
}
