using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Logic;
using Serialize;

namespace CarDealerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initial Load where GUI is loaded and comboStatusbox is initialized. It will also try to load XML files, if fail, then create new XML files
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            comboStatusV.Items.Add("Commission");
            comboStatusV.Items.Add("Sold");
            comboStatusV.Items.Add("Leased");
            BSContactBox.Visibility = System.Windows.Visibility.Hidden;
            BSFaxBox.Visibility = System.Windows.Visibility.Hidden;
            BSSEnr.Visibility = System.Windows.Visibility.Hidden;
            LabelContact.Visibility = System.Windows.Visibility.Hidden;
            labelFax.Visibility = System.Windows.Visibility.Hidden;
            labelSE.Visibility = System.Windows.Visibility.Hidden;
            boxCapacity.Visibility = Visibility.Collapsed;
            labelCapacity.Visibility = Visibility.Collapsed;
            boxWeight.Visibility = Visibility.Collapsed;
            labelWeight.Visibility = Visibility.Collapsed;
            
            
            
            //Load data from Persistent layer
            try
            {
                if (Persistent.DeSerializePC().Count() != 0)
                {
                    for (int i = 0; i < Persistent.DeSerializePC().Count(); i++)
                    {
                        oCarDealer.AddPrivateCustomer(Persistent.DeSerializePC()[i]);
                    }
                }
                else
                    Persistent.SerializePC(oCarDealer.GetPrivateCustomer());


                if (Persistent.DeSerializeBC().Count() != 0)
                {
                    for (int i = 0; i < Persistent.DeSerializeBC().Count(); i++)
                    {
                        oCarDealer.AddBusinessCustomer(Persistent.DeSerializeBC()[i]);
                    }
                }
                else
                    Persistent.SerializeBC(oCarDealer.GetBusinessCustomer());



                if (Persistent.DeSerializeCarSmall().Count() != 0)
                {
                    for (int i = 0; i < Persistent.DeSerializeCarSmall().Count(); i++)
                    {
                        oCarDealer.AddSmallCar(Persistent.DeSerializeCarSmall()[i]);
                    }
                }
                else
                    Persistent.SerializeCarSmall(oCarDealer.GetSmallCar());

                if (Persistent.DeSerializeCarLarge().Count() != 0)
                {
                    for (int i = 0; i < Persistent.DeSerializeCarLarge().Count(); i++)
                    {
                        oCarDealer.AddLargeCar(Persistent.DeSerializeCarLarge()[i]);
                    }
                }
                else
                    Persistent.SerializeCarLarge(oCarDealer.GetLargeCar());



                if (Persistent.DeSerializeTruck().Count() != 0)
                {
                    for (int i = 0; i < Persistent.DeSerializeTruck().Count(); i++)
                    {
                        oCarDealer.AddTruck(Persistent.DeSerializeTruck()[i]);
                    }
                }
                else
                    Persistent.SerializeTruck(oCarDealer.GetTruck());

                if (Persistent.DeSerializeContract().Count() != 0)
                {
                    for (int i = 0; i < Persistent.DeSerializeContract().Count(); i++)
                    {
                        oContract.AddContract(Persistent.DeSerializeContract()[i]);
                    }
                }
                else
                    Persistent.SerializeContract(oContract.GetContractList());

                if (Persistent.DeSerializeLeasing().Count() != 0)
                {
                    for (int i = 0; i < Persistent.DeSerializeLeasing().Count(); i++)
                    {
                        oLeasing.AddLeasing(Persistent.DeSerializeLeasing()[i]);
                    }
                }
                else
                    Persistent.SerializeLeasing(oLeasing.GetLeasingList());
            }
                //If fail to load, create new files
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("XML files created!");
                Persistent.SerializePC(oCarDealer.GetPrivateCustomer());
                Persistent.SerializeBC(oCarDealer.GetBusinessCustomer());
                Persistent.SerializeCarSmall(oCarDealer.GetSmallCar());
                Persistent.SerializeCarLarge(oCarDealer.GetLargeCar());
                Persistent.SerializeTruck(oCarDealer.GetTruck());
                Persistent.SerializeContract(oContract.GetContractList());
                Persistent.SerializeLeasing(oLeasing.GetLeasingList());
            }

            
        }
        /// <summary>
        /// Create objects
        /// </summary>
        CarDealer oCarDealer = new CarDealer();
        XML Persistent = new XML();
        Leasing oLeasing = new Leasing();
        Contract oContract = new Contract();


        /// <summary>
        /// Button for creating new costumers. It has conditions which is controlled by the radiobuttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (RBPrivate.IsChecked == true)
            {
                try
                {
                    //create private costumer
                    oCarDealer.AddPrivateCustomer(new PrivateCustomer(NameBox.Text, AddressBox.Text, int.Parse(phoneBox.Text), int.Parse(ageBox.Text), sexBox.Text));
                    //save it to xml file
                    Persistent.SerializePC(oCarDealer.GetPrivateCustomer());
                    MessageBox.Show("New Private Customer has been added");                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }  
            }
            if (RBBusiness.IsChecked == true)
            {
                try
                {
                    //create buissness costumer
                    oCarDealer.AddBusinessCustomer(new BusinessCustomer(NameBox.Text, AddressBox.Text, int.Parse(phoneBox.Text), int.Parse(BSSEnr.Text), int.Parse(BSFaxBox.Text), BSContactBox.Text) );
                    //save it to xml file
                    Persistent.SerializeBC(oCarDealer.GetBusinessCustomer());
                    MessageBox.Show("New Business Customer has been added");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (RBBusiness.IsChecked == false && RBPrivate.IsChecked == false)
            {
                MessageBox.Show("You need to choose a type of customer!");
            }
        }

        /// <summary>
        /// If clicked it show all the relevant info for adding Buisness costumers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBBusiness_Checked(object sender, RoutedEventArgs e)
        {
            BSContactBox.Visibility = System.Windows.Visibility.Visible;
            BSFaxBox.Visibility = System.Windows.Visibility.Visible;
            BSSEnr.Visibility = System.Windows.Visibility.Visible;
            LabelContact.Visibility = System.Windows.Visibility.Visible;
            labelFax.Visibility = System.Windows.Visibility.Visible;
            labelSE.Visibility = System.Windows.Visibility.Visible;
            labelSex.Visibility = System.Windows.Visibility.Hidden;
            labelAge.Visibility = System.Windows.Visibility.Hidden;
            sexBox.Visibility = System.Windows.Visibility.Hidden;
            ageBox.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// If clicked it show all the relevant info for adding Private costumers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBPrivate_Checked(object sender, RoutedEventArgs e)
        {
            BSContactBox.Visibility = System.Windows.Visibility.Hidden;
            BSFaxBox.Visibility = System.Windows.Visibility.Hidden;
            BSSEnr.Visibility = System.Windows.Visibility.Hidden;
            LabelContact.Visibility = System.Windows.Visibility.Hidden;
            labelFax.Visibility = System.Windows.Visibility.Hidden;
            labelSE.Visibility = System.Windows.Visibility.Hidden;
            labelSex.Visibility = System.Windows.Visibility.Visible;
            labelAge.Visibility = System.Windows.Visibility.Visible;
            sexBox.Visibility = System.Windows.Visibility.Visible;
            ageBox.Visibility = System.Windows.Visibility.Visible;
        }
       

        
        
        
        
        
        
        
        //CAR STUFF
        /// <summary>
        /// If clicked it show all the relevant info for adding trucks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBTruck_Checked(object sender, RoutedEventArgs e)
        {
            //Hide Checkbox
            cbSmall.Visibility = Visibility.Collapsed;
            cbLarge.Visibility = Visibility.Collapsed;

            //Hide textbox
            boxWeight.Visibility = Visibility.Collapsed;
            boxCapacity.Visibility = Visibility.Visible;

            //Hide Labels
            labelCapacity.Visibility = Visibility.Visible;
            labelWeight.Visibility = Visibility.Collapsed;


           
        }
        /// <summary>
        /// If clicked it show all the relevant info for adding cars and have conditions for small and large cars, which is defined by the radiobuttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBCar_Checked(object sender, RoutedEventArgs e)
        {
            //Show Checkbox
            cbSmall.Visibility = Visibility.Visible;
            cbLarge.Visibility = Visibility.Visible;

            if (cbSmall.IsChecked == true)
            {
                labelWeight.Visibility = Visibility.Visible;
                boxWeight.Visibility = Visibility.Visible;


                boxCapacity.Visibility = Visibility.Collapsed;
                labelCapacity.Visibility = Visibility.Collapsed;
            }
            if (cbLarge.IsChecked == true)
            {
                cbSmall.IsChecked = false;
                boxWeight.Visibility = Visibility.Collapsed;
                labelWeight.Visibility = Visibility.Collapsed;

                boxCapacity.Visibility = Visibility.Visible;
                labelCapacity.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// if clicked, LargeCar checkbox is hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSmall_Checked(object sender, RoutedEventArgs e)
        {
            cbLarge.IsChecked = false;
            labelWeight.Visibility = Visibility.Visible;
            boxWeight.Visibility = Visibility.Visible;


            boxCapacity.Visibility = Visibility.Collapsed;
            labelCapacity.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// if clicked smallCar checkbox is hidden 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbLarge_Checked(object sender, RoutedEventArgs e)
        {
            cbSmall.IsChecked = false;
            boxWeight.Visibility = Visibility.Collapsed;
            labelWeight.Visibility = Visibility.Collapsed;

            boxCapacity.Visibility = Visibility.Visible;
            labelCapacity.Visibility = Visibility.Visible;
        }



      
        /// <summary>
        /// Clears the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VCancelButton_Click(object sender, RoutedEventArgs e)
        {
            CarColor.Clear();
            CarModel.Clear();
            CarPrice.Clear();
           
        }

        /// <summary>
        /// shows list over costumers in the listbox. it's controlled by a radiobutton which type costumer is shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPCustomers_Click(object sender, RoutedEventArgs e)
        {
            if (ShowListBox.Items.Count != 0)
                ShowListBox.Items.Clear();
            
            if (RBPrivate.IsChecked == true)
            {
                //if not empty load data
                if (Persistent.DeSerializePC().Count() != 0)
                {
                    for (int i = 0; i < Persistent.DeSerializePC().Count(); i++)
                    {
                        ShowListBox.Items.Add("Name: " + Persistent.DeSerializePC()[i].name + "\r\n" + "Address: " + Persistent.DeSerializePC()[i].address + "\r\n" + "Phone no.: " + Persistent.DeSerializePC()[i].phone + "\r\n" + "Age: " + Persistent.DeSerializePC()[i].age + "\r\n" + "Sex: " + Persistent.DeSerializePC()[i].sex + "\r\n");
                    }
                }
                else
                {
                    ShowListBox.Items.Add("Empty database");
                }
            }
            if (RBBusiness.IsChecked == true)
            {
                //if not empty load data
                if (Persistent.DeSerializeBC().Count() != 0)
                {
                    for (int i = 0; i < Persistent.DeSerializeBC().Count(); i++)
                    {
                        ShowListBox.Items.Add("Company: " + Persistent.DeSerializeBC()[i].name + "\r\n" + "Address: " + Persistent.DeSerializeBC()[i].address + "\r\n" + "Phone no.: " + Persistent.DeSerializeBC()[i].phone + "\r\n" + "SE no.: " + Persistent.DeSerializeBC()[i].SEnr + "\r\n" + "Fax no. : " + Persistent.DeSerializeBC()[i].fax + "\r\n" + "Contact Person: " + Persistent.DeSerializeBC()[i].ContactPerson + "\r\n");
                    }
                }
                else
                {
                    ShowListBox.Items.Add("Empty database");
                }
            }
        }

        /// <summary>
        /// Clears the coustumer form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            NameBox.Clear();
            AddressBox.Clear();
            phoneBox.Clear();
            sexBox.Clear();
            ageBox.Clear();
            BSContactBox.Clear();
            BSFaxBox.Clear();
            BSSEnr.Clear();
        }





        //Vechicle Tab
        /// <summary>
        /// if clicked it adds a vehicle. which type of vechicle is controlled by radiobuttons and checkboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VOKButton_Click(object sender, RoutedEventArgs e)
        {
            if(RBCar.IsChecked == true)
            {
                try
                {
                    if (cbSmall.IsChecked == true)
                    {
                        ////create small car
                        oCarDealer.AddSmallCar(new Small(CarModel.Text, CarColor.Text, "Available", float.Parse(CarPrice.Text), int.Parse(boxWeight.Text), "NONE"));
                        //save it to xml file
                        Persistent.SerializeCarSmall(oCarDealer.GetSmallCar());
                        MessageBox.Show("New small car has been added");
                    }
                    if (cbLarge.IsChecked == true)
                    {
                        //create large car
                        oCarDealer.AddLargeCar(new Large(CarModel.Text, CarColor.Text, float.Parse(CarPrice.Text), "Available", int.Parse(boxCapacity.Text), "NONE"));
                        //save it to xml file
                        Persistent.SerializeCarLarge(oCarDealer.GetLargeCar());
                        MessageBox.Show("New large car has been added");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if(RBTruck.IsChecked == true)
            {
                try
                {
                    //create truck
                    oCarDealer.AddTruck(new Truck(CarModel.Text, CarColor.Text, float.Parse(CarPrice.Text), "Available", int.Parse(boxCapacity.Text), "NONE"));
                    //save it to xml file
                    Persistent.SerializeTruck(oCarDealer.GetTruck());
                    MessageBox.Show("New Truck has been added");                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }



        }


        // Located on the Vehicle Tab
        /// <summary>
        /// Shows a list over all the vehicles. Which type of Vehicles shown, is controlled by radiobuttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowVehicleList_Click(object sender, RoutedEventArgs e)
        {
            if (RBCar.IsChecked == true)
            {
                //clear list
                if (ShowVehicleList.Items.Count != 0)
                    ShowVehicleList.Items.Clear();

                //if not empty load data
                if (Persistent.DeSerializeCarSmall().Count() != 0)
                {
                    for (int i = 0; i < Persistent.DeSerializeCarSmall().Count(); i++)
                    {
                        ShowVehicleList.Items.Add("Car Model: " + Persistent.DeSerializeCarSmall()[i].model + "\r\n" + "Color: " + Persistent.DeSerializeCarSmall()[i].color + "\r\n" + "Price: " + Persistent.DeSerializeCarSmall()[i].price + " DDK" + "\r\n" + "Weight: " + Persistent.DeSerializeCarSmall()[i].weight + " kg" + "\r\n" + "Status: " + Persistent.DeSerializeCarSmall()[i].status + "\r\n" + "Owner: " + Persistent.DeSerializeCarSmall()[i].buyerName + "\r\n\n");
                    }
                }
                //if not empty load data
                if (Persistent.DeSerializeCarLarge().Count() != 0)
                {
                    for (int i = 0; i < Persistent.DeSerializeCarLarge().Count(); i++)
                    {
                        ShowVehicleList.Items.Add("Car Model: " + Persistent.DeSerializeCarLarge()[i].model + "\r\n" + "Color: " + Persistent.DeSerializeCarLarge()[i].color + "\r\n" + "Price: " + Persistent.DeSerializeCarLarge()[i].price +" DKK" +"\r\n" + "Capacity: " + Persistent.DeSerializeCarLarge()[i].largeCarCapacity +" people"+ "\r\n" + "Status: " + Persistent.DeSerializeCarLarge()[i].status + "\r\n" + "Owner: " + Persistent.DeSerializeCarLarge()[i].buyerName + "\r\n\n");
                    }
                }

                if(Persistent.DeSerializeCarLarge().Count() == 0 & Persistent.DeSerializeCarSmall().Count() == 0)
                {
                    ShowVehicleList.Items.Add("Empty database");
                }
            }

            if (RBTruck.IsChecked == true)
            {
                //clear list
                if (ShowVehicleList.Items.Count != 0)
                    ShowVehicleList.Items.Clear();
                //if not empty load data
                if (Persistent.DeSerializeTruck().Count() != 0)
                {
                    for (int i = 0; i < Persistent.DeSerializeTruck().Count(); i++)
                    {
                        ShowVehicleList.Items.Add("Car Model: " + Persistent.DeSerializeTruck()[i].model + "\r\n" + "Color: " + Persistent.DeSerializeTruck()[i].color + "\r\n" + "Price: " + Persistent.DeSerializeTruck()[i].price + " DDK" + "\r\n" + "Capacity: " + Persistent.DeSerializeTruck()[i].capacity + " km^3" + "\r\n" + "Status: " + Persistent.DeSerializeTruck()[i].status + "\r\n" + "Owner: " + Persistent.DeSerializeTruck()[i].buyerName + "\r\n\n");
                    }
                }
                if(Persistent.DeSerializeTruck().Count() == 0)
                {
                    ShowVehicleList.Items.Add("Empty database");
                }
            }

        }


        /// <summary>
        /// When selected it will load the small and large cars into the combobox, as well for the private costumers. since private costumers only can make contracts, leasing info is hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbPrivateSelc(object sender, RoutedEventArgs e)
        {
            selectCostomer.Items.Clear();
            selectVehicle.Items.Clear();

            labelPeriod.Visibility = Visibility.Collapsed;
            labelRent.Visibility = Visibility.Collapsed;
            boxRentMonth.Visibility = Visibility.Collapsed;
            boxPeriod.Visibility = Visibility.Collapsed;
            btnLeasing.Visibility = Visibility.Collapsed;
            btnContract.Visibility = Visibility.Visible;

            //if not empty load data
            if (Persistent.DeSerializePC().Count() != 0)
            {
                for (int i = 0; i < Persistent.DeSerializePC().Count(); i++)
                {
                    selectCostomer.Items.Add(Persistent.DeSerializePC()[i].name);
                }
            }
            if (Persistent.DeSerializePC().Count() == 0)
            {
                selectCostomer.Items.Add("Empty");
            }
            //if not empty load data
            if (Persistent.DeSerializeCarSmall().Count() != 0)
            {
                for (int i = 0; i < Persistent.DeSerializeCarSmall().Count(); i++)
                {
                    //ONLY AVAILABLE CAR CAN BE SELECTED
                    if (Persistent.DeSerializeCarSmall()[i].status == "Available")
                    {
                        selectVehicle.Items.Add(Persistent.DeSerializeCarSmall()[i].model);
                    }
                    
                }
            }
            //if not empty load data
            if (Persistent.DeSerializeCarLarge().Count() != 0)
            {
                for (int i = 0; i < Persistent.DeSerializeCarLarge().Count(); i++)
                {
                    //ONLY AVAILABLE CAR CAN BE SELECTED
                    if (Persistent.DeSerializeCarLarge()[i].status == "Available")
                    {
                        selectVehicle.Items.Add(Persistent.DeSerializeCarLarge()[i].model);
                    }
                }
            }
            if (Persistent.DeSerializeCarLarge().Count() == 0 & Persistent.DeSerializeCarSmall().Count() == 0)
            {
                selectVehicle.Items.Add("Empty");
            }
            
        }

        /// <summary>
        /// When selected it will load list of trucks into the combobox, as well for the buissness costumers. since buissness costumers only can make leasing, contract info is hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbBuisSel(object sender, RoutedEventArgs e)
        {
            labelPeriod.Visibility = Visibility.Visible;
            labelRent.Visibility = Visibility.Visible;
            boxRentMonth.Visibility = Visibility.Visible;
            boxPeriod.Visibility = Visibility.Visible;
            btnLeasing.Visibility = Visibility.Visible;
            btnContract.Visibility = Visibility.Collapsed;
            selectCostomer.Items.Clear();
            selectVehicle.Items.Clear();

            //if not empty load data
            if (Persistent.DeSerializeBC().Count() != 0)
            {
                for (int i = 0; i < Persistent.DeSerializeBC().Count(); i++)
                {
                    selectCostomer.Items.Add(Persistent.DeSerializeBC()[i].name);
                }
            }
            if (Persistent.DeSerializeBC().Count() == 0)
            {
                selectCostomer.Items.Add("Empty");
            }
            //if not empty load data
            if (Persistent.DeSerializeTruck().Count() != 0)
            {
                for (int i = 0; i < Persistent.DeSerializeTruck().Count(); i++)
                {
                    if (Persistent.DeSerializeTruck()[i].status == "Available")
                    {
                        selectVehicle.Items.Add(Persistent.DeSerializeTruck()[i].model);
                    }
                }
            }
            if (Persistent.DeSerializeTruck().Count() == 0)
            {
                selectVehicle.Items.Add("Empty");
            }

        }

        /// <summary>
        /// when clicked it create contract for private costumers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //create contract
                oContract.AddContract(new Contract(selectCostomer.SelectedItem.ToString(), selectVehicle.SelectedItem.ToString(), DateTime.Now));
                //save it to xml file
                Persistent.SerializeContract(oContract.GetContractList());

                for (int i = 0; i < Persistent.DeSerializeCarLarge().Count(); i++)
                {
                    //Update CarLarge and save to xml file
                    if (selectVehicle.SelectedItem.ToString() == Persistent.DeSerializeCarLarge()[i].model)
                    {
                        oCarDealer.GetLargeCar()[i].status = comboStatusV.SelectedItem.ToString();
                        oCarDealer.GetLargeCar()[i].buyerName = selectCostomer.SelectedItem.ToString();
                        Persistent.SerializeCarLarge(oCarDealer.GetLargeCar());
                    }
                }
                for (int i = 0; i < Persistent.DeSerializeCarSmall().Count(); i++)
                {
                    //Update CarSmall and save to xml file
                    if (selectVehicle.SelectedItem.ToString() == Persistent.DeSerializeCarSmall()[i].model)
                    {
                        oCarDealer.GetSmallCar()[i].status = comboStatusV.SelectedItem.ToString();
                        oCarDealer.GetSmallCar()[i].buyerName = selectCostomer.SelectedItem.ToString();
                        Persistent.SerializeCarSmall(oCarDealer.GetSmallCar());
                    }
                }

                MessageBox.Show("Contract Created");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        /// <summary>
        /// when clicked it create leasing for buissness costumers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeasing_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //create leasing
                oLeasing.AddLeasing(new Leasing(selectVehicle.SelectedItem.ToString(), selectCostomer.SelectedItem.ToString(), int.Parse(boxRentMonth.Text), int.Parse(boxPeriod.Text), DateTime.Now));
                //save it to xml file
                Persistent.SerializeLeasing(oLeasing.GetLeasingList());

                for (int i = 0; i < Persistent.DeSerializeTruck().Count(); i++)
                {
                    //Update Truckinfo and save to xml file
                    if (selectVehicle.SelectedItem.ToString() == Persistent.DeSerializeTruck()[i].model)
                    {
                        oCarDealer.GetTruck()[i].status = comboStatusV.SelectedItem.ToString();
                        oCarDealer.GetTruck()[i].buyerName = selectCostomer.SelectedItem.ToString();
                        Persistent.SerializeTruck(oCarDealer.GetTruck());
                    }
                }
                MessageBox.Show("Leasing Created");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        /// <summary>
        /// when clicked it shows list of contracts or leasing. what is shown is dependent on the radiobuttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowContracts_Click(object sender, RoutedEventArgs e)
        {
            if (rbPrivateSel.IsChecked == true)
            {
                //clear list
                listboxContracts.Items.Clear();
                listboxContracts.Items.Add("Car Contracts for private costomers");
                //if not empty load data
                if (Persistent.DeSerializeContract().Count() != 0)
                {
                    for (int i = 0; i < Persistent.DeSerializeContract().Count(); i++)
                    {
                        listboxContracts.Items.Add("Car Model: " + Persistent.DeSerializeContract()[i].ContractCarModel + "\r\n" + "Owner: " + Persistent.DeSerializeContract()[i].ContractBuyerName + "\r\n" + "Sale Date: " + Persistent.DeSerializeContract()[i].ContractBuyDate + "\r\n\n");
                    }
                }
                if (Persistent.DeSerializeContract().Count() == 0)
                {
                    listboxContracts.Items.Add("Empty");
                }

            }
            if (rbBuisSelc.IsChecked == true)
            {
                //clear list
                listboxContracts.Items.Clear();
                listboxContracts.Items.Add("Truck leasing for buisness costumers:");
                //if not empty load data
                if (Persistent.DeSerializeLeasing().Count() != 0)
                {
                    for (int i = 0; i <Persistent.DeSerializeLeasing().Count() ; i++)
                    {
                        listboxContracts.Items.Add("Car Model: " + Persistent.DeSerializeLeasing()[i].LeasingCarModel + "\r\n" + "Owner: " + Persistent.DeSerializeLeasing()[i].LeasingBuyerName + "\r\n" + "Rent pro Month: " + Persistent.DeSerializeLeasing()[i].LeasingrenthMonth + "\r\n" + "Rent Period: " + Persistent.DeSerializeLeasing()[i].LeasingrentPeriod + " months" + "\r\n" + "Start rent date: " + Persistent.DeSerializeLeasing()[i].LeasingstartDate);
                    }
                }
                if (Persistent.DeSerializeLeasing().Count() == 0)
                {
                    listboxContracts.Items.Add("Empty");
                }           
            }

        }            
    }  
}
