using Pract_15092023.DAL.Entity;
using Pract_15092023.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_15092023
{
    public class CardProvider
    {
        private readonly IRepository<StudentCard> _repository;



        public CardProvider(IRepository<StudentCard> repository)
        {
            _repository = repository;
        }



        public void AddCards(List<StudentCard> cards)
        {
            cards.ForEach(card => AddCard(card));
        }



        public void AddCard(StudentCard card)
        {
            _repository.Add(card);
        }



        public void RemoveCards(List<StudentCard> cards)
        {
            cards.ForEach(card => RemoveStudent(card));
        }



        public void RemoveStudent(StudentCard card)
        {
            _repository.Remove(card);
        }



        public void RemoveStudent(int Id)
        {
            var card = GetStudent(Id);
            _repository.Remove(card);
        }



        public StudentCard GetStudent(int Id)
        {
            return _repository.Get(Id);
        }



        public IEnumerable<StudentCard> GetCards()
        {
            return _repository.GetAll();
        }
    }
}
