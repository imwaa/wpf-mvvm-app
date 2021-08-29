using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MvvmDemo.Models;
using MvvmDemo.Commands;
using System.Collections.ObjectModel;
using AutoMapper;

namespace MvvmDemo.ModelViews
{
    public class EmployeeViewModel:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


        EmployeeService ObjEmployeeService;
        public EmployeeViewModel()
        {
            ObjEmployeeService = new EmployeeService();
            LoadData();
            CurrentEmployee = new Employee();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }

        #region DisplayOperation
        private ObservableCollection<Employee> employeesList;
            
        public ObservableCollection<Employee> EmployeesList
        {
            get { return employeesList; }
            set { employeesList = value; onPropertyChanged("EmployeesList"); }
        }
         
        private void LoadData()
        {
            EmployeesList = new ObservableCollection<Employee>(ObjEmployeeService.GetAll());
        }
        #endregion


        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; onPropertyChanged("Message"); }
        }
         
        private Employee currentEmployee;

        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { currentEmployee = value;onPropertyChanged("CurrentEmployee"); }
        }
        #region saveOperation
        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        public void Save()
        {
            try
            {
                Employee Employee2save = new Employee();
                Employee2save = Map(currentEmployee, Employee2save);

                var IsSaved = ObjEmployeeService.Add(Employee2save);
                LoadData();
                if (IsSaved)
                {
                    Message = "Employee saved";

                }
                else
                {
                    Message = "Save operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region SearchOperation
        private RelayCommand searchCommand;

        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }

        public void Search()
        {
            try
            {
                //Employee ObjEmployee = new Employee();
                var ObjEmployee = ObjEmployeeService.Search(currentEmployee.Id);
                if (ObjEmployee != null)
                {
                    CurrentEmployee = Map(ObjEmployee, CurrentEmployee);
                    //CurrentEmployee.Name = ObjEmployee.Name;


                }
                else
                {
                    Message = "Employee not found";
                }
            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }
        }
        #endregion


        #region UpdateOperation

        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        public void Update()
        {
            try
            {
                Employee employee2update = new Employee();
                employee2update = Map(CurrentEmployee, employee2update);

                var IsUpdated = ObjEmployeeService.Update(employee2update);
                if (IsUpdated)
                {
                    Message = "Employee Updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        #endregion



        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }

        public void Delete()
        {
            try
            {
                Employee employee2delete = new Employee();
                employee2delete = Map(CurrentEmployee, employee2delete);

                bool IsDeleted = ObjEmployeeService.Delete(employee2delete.Id);
                if (IsDeleted)
                {
                    Message = "Employee deleted";
                    LoadData();
                }
                else
                {
                    Message = "Employee not deleted";
                }
            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }
        }

        public static TU Map<T, TU>(T source, TU destination)
        {
            // configure le mapper
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, TU>());

            var mapper = config.CreateMapper(); // crée le mapper
            return mapper.Map<TU>(source); // map et retourne le résultat
        }


    }
}
