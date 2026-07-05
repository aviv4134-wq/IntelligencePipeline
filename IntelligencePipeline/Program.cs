using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Pipeline;
using IntelligencePipeline.Storage;
using System;


namespace IntelligencePipeline
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("WELCOME TO OUR PROGRAM");


            ReportPipeline pipline = new ReportPipeline();
            ReportRepository reportRepository = pipline.GetValidatedReports();
            RejectedReportRepository rejectedReportRepository = pipline.GetRejectedReports();

            bool run = true;
            while (run)
            {
                Console.WriteLine("""
                1.add report
                2.show validated reports
                3.serch report by keyword in discription
                4.filter report by status
                5.update report status
                6.show report ditales
                7. show rejected reports
                8.show statistics
                9.sort by parameter

                enter number between 1 - 9:
                """);
                string userInputMenu = Console.ReadLine();

                switch (userInputMenu)
                {
                    case "1":

                        Console.WriteLine("enter dateTime\nlatitude\nlongitude:");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateTime))
                            Console.WriteLine("error invalid datetime");

                        else if (!double.TryParse(Console.ReadLine(), out double latitude))
                            Console.WriteLine("error only numbers allowed with . in latitude");

                        else if (!double.TryParse(Console.ReadLine(), out double longitude))
                            Console.WriteLine("error only numbers allowed with . in longtude");
                        else
                        {
                            Console.WriteLine("enter discirption: ");
                            string description = Console.ReadLine();
                            Console.WriteLine("1.Drone\n2.Signal\n3.Soldier\n4.Radar\nenter 1 - 4");
                            int reportIdAuto = pipline.NextReportId;
                            string reportTypeNumber = Console.ReadLine();
                            switch (reportTypeNumber)
                            {
                                case "1":
                                    Console.WriteLine("enter altitude imageQualty: ");
                                    if (!int.TryParse(Console.ReadLine(), out int altitude))
                                        Console.WriteLine("error only hole numbers allowed in altitude ");

                                    else if (!int.TryParse(Console.ReadLine(), out int imageQualty))
                                        Console.WriteLine("error only hole numbers allowed in imageQualty");
                                    else
                                    {
                                        DroneReport reportDrone = new DroneReport(reportIdAuto, dateTime, latitude, longitude, description, altitude, imageQualty);
                                        pipline.ProcessReport(reportDrone);

                                    }
                                    break;



                                case "2":

                                    Console.WriteLine("enter frequency\nlangauge\nsignalStrength : ");
                                    if (!double.TryParse(Console.ReadLine(), out double frequency))
                                        Console.WriteLine("error only numbers with . allowed in frequency ");


                                    else if (!Enum.TryParse(Console.ReadLine(), true, out Language language))
                                        Console.WriteLine("error only those languages allowed Hebrew,Arabic,English,Russian,Other");

                                    else if (!int.TryParse(Console.ReadLine(), out int signalStrength))
                                        Console.WriteLine("error only hole numbers allowed in signalStrength");

                                    else
                                    {
                                        Console.WriteLine("enter content: ");
                                        string content = Console.ReadLine();
                                        SignalReport reportSignal = new SignalReport(reportIdAuto, dateTime, latitude, longitude, description, frequency, content, language, signalStrength);
                                        pipline.ProcessReport(reportSignal);

                                    }
                                    break;

                                case "3":
                                    Console.WriteLine("enter soldier Name: ");
                                    string soldierName = Console.ReadLine();

                                    Console.WriteLine("enter soldier Id: ");
                                    string soldierId = Console.ReadLine();

                                    Console.WriteLine("enter unit:");
                                    string unit = Console.ReadLine();

                                    Console.WriteLine("enter confidenceLevel:");
                                    if (!int.TryParse(Console.ReadLine(), out int confidenceLevel))
                                        Console.WriteLine("error only hole numbers allowed in confidenceLevel");

                                    else
                                    {
                                        SoldierReport reportSoldier = new SoldierReport(reportIdAuto, dateTime, latitude, longitude, description, soldierName, soldierId, unit, confidenceLevel);
                                        pipline.ProcessReport(reportSoldier);
                                    }

                                    break;

                                case "4":
                                    Console.WriteLine("enter speed\ndirection\ndistanceUser");
                                    if (!int.TryParse(Console.ReadLine(), out int speed))
                                        Console.WriteLine("error only hole numbers allowed in speed");

                                    else if (!int.TryParse(Console.ReadLine(), out int direction))
                                        Console.WriteLine("error only hole numbers allowed in direction");

                                    else if (!int.TryParse(Console.ReadLine(), out int distance))
                                        Console.WriteLine("error only hole numbers allowed in distance");
                                    else
                                    {
                                        RadarReport reportRadar = new RadarReport(reportIdAuto, dateTime, latitude, longitude, description, speed, direction, distance);
                                        pipline.ProcessReport(reportRadar);

                                    }
                                    break;

                                default:

                                    Console.WriteLine("error only 1 - 4 allowed");
                                    break;

                            }

                        }
                        break;


                    case "2":
                        {
                            DisplayValidatedReports(reportRepository);
                            break;
                        }

                    case "3":
                        {
                           Console.WriteLine("enter keyword:");
                            string keyWordUser = Console.ReadLine();
                            List<Report> reportsBykeyWord = reportRepository.Search(keyWordUser);
                                    foreach (Report reportBykeyword in reportsBykeyWord)
                                        DisplayReport(reportBykeyword);
                                    break;
                        }

                            case "4":
                                {
                                    Console.WriteLine("enter status");

                                    if (!Enum.TryParse(Console.ReadLine(), true, out ReportStatus status))
                                        Console.WriteLine("error status must be  New, Validating, Validated, Rejected,InProgress,Completed");
                                    else
                                    {
                                        List<Report> reportsByStatus = reportRepository.GetByStatus(status);
                                        foreach (Report reportByStatus in reportsByStatus)
                                            DisplayReport(reportByStatus);
                                    }
                                    break;
                                }

                            case "5":
                                {
                                    Console.WriteLine("enter status\nuser id:");
                                    if (!Enum.TryParse(Console.ReadLine(), true, out ReportStatus status))
                                        Console.WriteLine("error status must be  New, Validating, Validated, Rejected,InProgress,Completed");

                                    else if (!int.TryParse(Console.ReadLine(), out int reportIdToUpdate))
                                        Console.WriteLine("error only hole numbers allowed in report ID");
                                    else
                                    {
                                        reportRepository.UpdateStatus(reportIdToUpdate, status);
                                    }
                                    break;
                                }

                            case "6":

                                if (!int.TryParse(Console.ReadLine(), out int reportId))
                                    Console.WriteLine("error only hole numbers allowed in report ID");

                                else
                                {
                                    Report? report = reportRepository.GetById(reportId);
                                    if (report == null) Console.WriteLine("error report not found");
                                    else DisplayReport(report);
                                }
                                break;

                            case "7":
                                DisplayRejectedReports(rejectedReportRepository);
                               break;

                            case "8":
                                pipline.DisplayStatistics();
                                break;
                            default:
                        Console.WriteLine("error");
                        break;


                }

            }
        }

        private static void DisplayReport(Report report)
        {
            Console.WriteLine(report.ToString());
        }
        private static void DisplayValidatedReports(ReportRepository repository)
        {
            foreach (Report report in repository.GetAll())
                Console.WriteLine(report.ToString());
        }
        private static void DisplayRejectedReports(RejectedReportRepository repository)
        {
            foreach (Report report in repository.GetAll())
                Console.WriteLine(report.ToString());
        }

    
    }

}

