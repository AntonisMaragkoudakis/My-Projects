#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <netinet/in.h>
#include <netdb.h>
#include <arpa/inet.h>


int main ( int argc , char * argv []) 
{
	
	char* programName;
	char serverName[100], inputFileWithCommands[100];
	int serverPORT, receivePort;

    if( argc == 5 )      // An exw swsto arithmo orismatwn synexizw stin dimiourgia tou Client alliws ektypwno lathos (sto telos tou kwdika)
    {
      	programName = argv[0];
      	//printf("The programName is %s\n", programName);
     	strcpy(serverName, argv[1]);
     	//printf("The serverName is %s\n", serverName);
      	serverPORT = atoi(argv[2]);
      	//printf("The serverPORT is %d\n", serverPORT);
      	receivePort = atoi(argv[3]);
      	//printf("The receivePort is %d\n", receivePort);
      	strcpy(inputFileWithCommands,argv[4]);
      	//printf("The inputFileWithCommands is %s\n\n", inputFileWithCommands);


	 	///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		int clientSocket,ret,recieve_Socket;
		struct sockaddr_in serverAddr,bind_addr,return_addr;
		struct hostent * rem ;

		clientSocket = socket(AF_INET, SOCK_STREAM, 0); // dimiourgw to socket tis TCP syndeshs pou tha stelnei ston Server
		if(clientSocket < 0)
		{
			perror("[-]Error in connection.");
			exit(EXIT_FAILURE);
		}
		printf("[+]Client Socket is created.\n");
		
		/* Find server addonoma_arxeious */
		if (( rem = gethostbyname(serverName) ) == NULL ) // pairnei to onoma tou server dld to serverName
		{    
			herror ( " gethostbyname " ) ; 
			exit (1) ;
		}

		memset(&serverAddr, '\0', sizeof(serverAddr));        // setarw to serverAddr poy tha eksipiretei thn epoikinwnia
		serverAddr.sin_family = AF_INET;
		memcpy (& serverAddr.sin_addr , rem->h_addr , rem->h_length ) ;
		serverAddr.sin_port = htons(serverPORT);   // dixnw to serverPort pou tha apefthinete o Client
		
		// Creating socket file descriptor 
		if ( (recieve_Socket = socket(AF_INET, SOCK_DGRAM, 0)) < 0 )    // anoigw socket gia thn udb syndesh me xrhsh SOCK_DGRAM 
		{  																// apo aythn tha perimenei tis apantiseis tou server h diergasia paidi
			perror("socket creation failed"); 
			exit(EXIT_FAILURE); 
		} 
		      
		memset(&bind_addr, 0, sizeof(bind_addr));     // setarisma mnhmhs bind
		memset(&return_addr, 0, sizeof(return_addr)); // setarisma mnhmhs return_addr apou tha lamvanei o client sto recievePort mesw udb
		      
		// Filling server information 
		bind_addr.sin_family    = AF_INET;           // IPv4 
		bind_addr.sin_addr.s_addr = INADDR_ANY; 
		bind_addr.sin_port = htons(receivePort);     // ftiaxnei ta stoixeia tis udb me port to recievePort
		      
		// Bind the socket with the server addonoma_arxeious 
		if ( bind(recieve_Socket, (const struct sockaddr *)&bind_addr, sizeof(bind_addr)) < 0 )  // bind
		{ 
			perror("bind failed"); 
			exit(EXIT_FAILURE); 
		} 
	      

		ret = connect(clientSocket, (struct sockaddr*)&serverAddr, sizeof(serverAddr));  // zhtame aitish gia sindesh me server mesw tcp
		if(ret < 0)
		{
			perror("[-]Error in connection.\n");
			exit(EXIT_FAILURE);
			//exit(1);
		}
		printf("[+]Connected to Server.\n");
	 	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	   


	    char buffer[1024];
		int i=0, line=0, recieve_size, bytes_recived;
	    char line_str[100], for_send[1024], receivePort_str[100];
	    sprintf(receivePort_str, "%d", receivePort);
		char temp_buffer[1024];
		char* infile;
		char* diefthinsh_seira;
		char* output_dot={"output."};

		FILE *fp1,*fp2;
	    fp1 = fopen(inputFileWithCommands,"r");

		pid_t parent_id;       // gennish mias diergasias typou pid_t me xrhsh fork()
		parent_id = fork();   // gia to paidi parent_id=0 enw gia ton patera parent_id=pid_tou_paidiou_tou

		while(1)
		{
			if (parent_id!=0) // an eisai pateras stelne ston server tis entoles
			{  
				while( fgets ( buffer, sizeof(buffer), fp1) != NULL) 
	    		{
					line++;
		      		i++;
		      		buffer[strlen(buffer)-1]= '\0';
		   			sprintf(line_str, "%d", line); 
					strcpy(for_send,receivePort_str);
					strcat(for_send,".");
					strcat(for_send,line_str);   // ftiaxe ayto pou einai na steileis
					strcat(for_send,"$");
					strcat(for_send,buffer);
					//printf("for sending: %s", for_send);
					send(clientSocket, for_send, 1024, 0);    //to for_send periexei format p.x recievePort.line$entolh
					if(strcmp(buffer, ":exit") == 0)
					{
						close(clientSocket);
						printf("[-]Disconnected from server.\n");    // Mhnyma aposyndeshs apo ton server
						exit(1);
					}
					bzero(for_send, sizeof(for_send));  // midenismos tou for_send

					if (i==10) //stelne 10-10 entoles ana 5 second
					{
						i=0;
						sleep(5);
					}	
				}
			}
			else    // aliws an eisai paidi perimene na akouseis me udb
			{            
	    		recieve_size = sizeof(return_addr);  
	    		bytes_recived = recvfrom(recieve_Socket, (char *)buffer, 1024, MSG_WAITALL, ( struct sockaddr *) &return_addr, &recieve_size); // recieve_Socket h udb apanthsh na labei ton buffer sto recieve_Socket
	    		//printf("received data : %s\n", buffer); 
	    		buffer[bytes_recived] = '\0';      //  to n einai to megethos tou buffer kai teleiwnei me \0                                                                          
				strcpy(temp_buffer,buffer); 
				
				diefthinsh_seira= strtok(temp_buffer,"$");    // strtok vriskei to prwto "$" kai pairnw to diefthinsh_seira mexri ekei
				char* onoma_arxeiou = malloc( strlen(output_dot) + strlen(diefthinsh_seira) + 1); // desmefsh mnhmhs gia to onoma_arxeiou
				strcpy(onoma_arxeiou, output_dot);    // opou output_dot = "output."
				strcat(onoma_arxeiou, diefthinsh_seira);  // ara to onoma edw swsta periexei output.recievePort.line
				strtok(buffer,"$");
				infile =strtok(NULL,"$");    // pairnw to aristero meros meta to "$" tou buffer to opoio kai tha grapsw mesa sto arxeio
				if(infile == NULL)
				{
					fp2=fopen(onoma_arxeiou,"w");   // anoigw to file fp2 me onoma onoma_arxeiou alla den grafw kati giati einai lathos entolh
				}
				else
				{              
					//printf("periexomeno:\n%s\n", infile);
					fp2=fopen(onoma_arxeiou,"w");   // anoigw to file fp2 me onoma onoma_arxeiou                     
					fputs(infile,fp2);              // grafw sto arxeio to infile opou periexei thn ektelesh mias swsths entolhs
				}
				fclose(fp2); // kleise to arxeio
			}
		}
		close(recieve_Socket); // otan termatistei h leitoyrgeia tou Client dhladh otan termatitei h while(1) tote kleise to socket tis epoikinwnias
    }
    else if( argc > 5 ) 
    {
      printf("Too many arguments supplied.\n\n");
    }
    else                                               // Ektyposh lathous arithmou orismatwn. Tipota apo ta parapanw den tha exei simvei an
    {                                                  // exei symvei ayto 
      printf("More arguments are expected.\n\n");
    }

    return 0;
}
