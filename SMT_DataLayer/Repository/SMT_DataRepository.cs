using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMT_DataLayer;
using SMT_DataLayer.Data;
namespace SMT_DataLayer.Repository
{
    public class SMT_DataRepository
    {
        SMT_DataContext _context;
        public SMT_DataRepository()
        {
            _context = new SMT_DataContext();
        }
        public bool AddCreditor(Creditor creditor)
        {
            try
            {
               if(_context.Creditors != null)
                {
                    _context.Creditors.Add(creditor);
                    _context.SaveChange();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool AddDeptor(Deptor deptor)
        {
            try
            {
                if (_context.Deptors != null)
                {
                    _context.Deptors.Add(deptor);
                    _context.SaveChange();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
