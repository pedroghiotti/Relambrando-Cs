
Console.WriteLine("Bem vindo ao estacionamento Otnemanoicatse!");

ParkingLot Estacionamento = new ParkingLot(1);
string? menuInput;

do {
    Console.Clear();
    Console.WriteLine("1 - Adicionar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("4 - Finalizar");
    Console.Write("> ");
    
    menuInput = Console.ReadLine();
    string? plate;

    switch(menuInput) {
        case "1":
            Console.WriteLine("Placa do veículo:");
            plate = Console.ReadLine();
            if(plate == null) {
                Console.WriteLine("Input inválido.");
                Console.ReadKey();
            }
            else {
                Estacionamento.CheckIn(plate);
            }
            break;
        case "2":
            Console.WriteLine("Placa do veículo:");
            plate = Console.ReadLine();
            if(plate == null) {
                Console.WriteLine("Input inválido.");
                Console.ReadKey();
            }
            else {
                float? checkOutFee = Estacionamento.CheckOut(plate);
                if(checkOutFee == null) break;
                else Console.WriteLine($"A taxa de estacionamento é de {checkOutFee}\nVolte sempre!");
                Console.ReadKey();
            }
            break;
        case "3":
            Estacionamento.ListVehicles();
            Console.ReadKey();
            break;
        default: break;
    }

} while(menuInput != "4");



class ParkingLot {
    private List<ParkingRegister> parkedVehicles = new List<ParkingRegister>();
    private float hourlyFee;

    public ParkingLot(float hourlyFee) {
        this.hourlyFee = hourlyFee;
    }

    public void CheckIn(string vehiclePlate) {
        ParkingRegister register = new ParkingRegister(vehiclePlate);
        parkedVehicles.Add(register);
    }

    public float? CheckOut(string vehiclePlate) {
        ParkingRegister? register = parkedVehicles.Find((ParkingRegister reg) => reg.vehiclePlate == vehiclePlate);
        if(register == null) {
            Console.WriteLine($"Nenhum veículo encontrado com a placa {vehiclePlate}.");
            Console.ReadKey();
            return null;
        }
        else {
            parkedVehicles.Remove(register);
            float stayTime = (float) (DateTime.Now - register.checkInTime).TotalHours;
            float checkOutFee = stayTime * this.hourlyFee;
            return checkOutFee;
        }
    }

    public void ListVehicles() {
        Console.WriteLine($"\nEstão estacionados {parkedVehicles.Count} veículos.\nSuas placas são:");
        foreach(ParkingRegister register in parkedVehicles) {
            Console.WriteLine($"{register.vehiclePlate}");
        }
    }
}

class ParkingRegister {
    public string vehiclePlate {get; private set;}
    public DateTime checkInTime {get; private set;}

    public ParkingRegister(string plate) {
        this.vehiclePlate = plate;
        this.checkInTime = DateTime.Now;
    }
}
