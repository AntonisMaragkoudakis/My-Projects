diction={} # Αρχικοποίηση των dictionaries
diction2={}
while True: #While true εκτύπωσης του μενού εώς ότου τερματιστεί το πρόγραμα με επιλογή 4 του χρήστη. 
    choice = input("Give your preference: (1: read new input file, 2: print statistics for a specific product, 3: print statistics for a specific AFM, 4: exit the program): ")
    if choice == '1':                                                  # Για επιλογή χρήστη το 1
        file_name = input("Give New Input File Name: ")                # Ζητάω και παίρνω το όνομα αρχείου που δίνει ο χρήστης
        try:
            file = open(file_name, "r", encoding='utf-8')              # Αν υπάρχει το αρχείο που ζειτάει ο χρήστης το ανοίγω
        except:                                                        # Αλλιώς συνεχίζω χωρίς να κάνω κάτι
            continue                                                   
        else:
            line_count=0        #Αρχικοπιώ όλες τις μεταβλητές ελέγχου.
            flag=1
            flagg=0
            flag2=0
            flag3=1
            flag4=0
            flag5=0
            flag_afm=0
            total_prize=0
            format_flag=1
            products = []
            exc=0
            while True:                           #Συνεχώς
                line_count=line_count+1
                line = file.readline().upper()    #Αναλύω το αρχείο σε σειρές
                if not line:
                    break                         #Και τελειώνω όταν δεν υπάρχει επόμενη σειρά.!

                word_count=0
                this_line = line.split()          # Split string into a list...... Πλέον το this_line είναι μία λίστα από λέξεις της σειράς line_count
                for word in this_line:            # Για κάθε λέξη της γρμμής αυτής
                    word_count = word_count + 1   # Ανεβάζω κατά ένα το word_count αφού βρίσκομαι πλέον στην επόμενη λέξη




####################### Εδώ ανάλογα σε ποιό line_count βρίσκομαι, ελέγχω αν το word_count έχει ξεπεράσει τον αναμενόμενο αριθμό λέξεων για την σειρά αυτή. 
                    if ( (line_count==1 and word_count==1) or (line_count==2 and word_count<3) or (line_count>2 and word_count<5 and flag4==0) or (word_count<3 and flag4==1)): 
                        format_flag=format_flag*1
                    else:                           
                        format_flag=0   # Λάθος φορμάτ απόδειξης όταν μπει έστω και μια φορά εδώ στην συνέχεια πάντα θα έχω format_flag=0
                                        # αφού έπειτα format_flag=format_flag*1 θα δίνει 0 μέχρι την επόμενη απόδηξη.!



#################### Εδώ όταν line_count=1 παίρνω flag=1 αν έχω μόνο "-" στην σειρά αυτή. Έτσι ελέγχω άν η πρώτη σειρά της απόδηξης περιέχει μόνο "------"
                    if line_count==1:                                                   
                        line1= line[:-1]         # line[:-1] αφού ο το τελευταίος χαρακτήρας περιέχει "/n" και δεν τον θέλω.
                        for character in line1:
                            if character!='-':   # Στον πρώτο χαρακήρα που δεν είναι "-" θα πάρω flag=0 και άρα δεν θα έχω έγκυρη απόδηξη.
                                flag=0
                                break
                    elif word_count==1:             # Αλλιώς αν δέν έχω line_count==1 ουσιαστικά έχω line_count>1 και ελέγχο μήπως η σειρά αυτή είναι η τελευταία της 
                        last_line = line[:-1]       # απόδειξης που ελέγχω. Αν λοιπόν περιέχει μόνο "-" τότε είναι η τελευταία και βάζω flagg=1.
                        flagg=1
                        for character in last_line:
                            if character!='-':   # για κάθε καινούρια σειρά αν flagg=0 ξέρω ότι δεν έφτασα στο τέλος της συγκεκριμένης απόδειξης.
                                flagg=0
                                break
                        if flag4==1 and flagg==0: # Εδώ απο την στιγμή που θα έχω flag4=1 δηλαδή θα έχω περάσει την πρό-τελευταία σειρά της απόδηξης (flag4 βλέπε παρακάτω)
                           flag4=2                # και αν έχω flagg=0 δηλαδή δεν έχω τελευταία σειρά "---" όπως θα έπρεπε, τότε βάζω το flag4=2 που σημαινει                                              
                                                  # ότι η απόδειξη είναι άκυρη αφου στην επόμενη σειρά απο την λέξη 'ΣΥΝΟΛΟ:' η απόδηξη συνεχίζει με προιόντα αντί να τελειώνει. 

#########################  Εδώ αρχίζω να ελέγχω κάθε λέξη (word) άν είναι έγκυρη και ταυτόχρονα αποθηκεύω τις χρήσημες πληροφορίες στα temp_AFM και products[]

################### Ταυτόχρονα παίζω με διάφορα flags για να διασφαλίσω τι γεγονότα έχουν προηγηθεί. Εδώ flag2=1 σημαίνει ότι έχω πάρει σωστά την λέξη 'ΑΦΜ:'
                    if (line_count==2 and word_count==1 and word=='ΑΦΜ:' and flag==1):      
                        flag2=1


#################  Εδώ η λέξη πρέπει αν περιέχει το 10ψήφιο αφμ. Αν τερματίσει με flag_afm=10 τότε το αφμ είναι σωστό. Χαζός τρόπος αλλά για κάποιο λόγο δεν μου δούλευε με try: except:
                    elif (line_count==2 and word_count==2 and len(word)==10):  
                        temp_AFM=word
                        for i in range(10):
                            if word[i]=='1' or word[i]=='2' or word[i]=='3' or word[i]=='4' or word[i]=='5' or word[i]=='6' or word[i]=='7' or word[i]=='8' or word[i]=='9' or word[i]=='0':
                                flag_afm=flag_afm+1
                                

################### Εδώ word!='ΣΥΝΟΛΟ: άρα το word πρέπει να περιέχει όνομα προιόντος.
                    elif (line_count>2 and word_count==1 and flagg!=1 and word!='ΣΥΝΟΛΟ:'):   
                        yparxei=0    
                        for i in range(0, len(products)-1, 2):  # Η λίστα products[] περιέχει [Ονόματα_Προιόντων Ποσότητες]. Άρα στις ζυγές θέσεις περιέχει τα ονόματα. 
                            if products[i]==word[:-1]:          # Η λέξη ενός ονόματος θα τελειώνει μόνο με ":" εξού και word[:-1]. Έτσι αν το όνομα υπάρχει ήδη στην
                                yparxei=1                       # λίστα από προηγούμενη γραμμή της ίδιας απόδηξης τότε αυτό υπάρχει στην θέση i της λίστας products[]
                                sthn_thesh=i
                                break

                        if yparxei==0:                  # Αν για την απόδηξη αυτή το όνομα του προιόντος εμφανίζεται πρώτη φορά τότε αποθηκεύετε στο τέλος της λιστας 
                            products.append(word[:-1])  # Αποθηκεύεται όμως χωρίς τον τελευταίο χαρακτήρα που αν έχω σωστό φορμάτ θα είναι ":"
                                
                        if word[-1]==':':  
                            flag3=flag3*1   # Αν μπεί έστω και μία φορά στο else απο εκεί και πέρα το flag3 θα είναι ίσο με 0 για αυτήν την απόδειξη 
                        else:               # Αυτό θα σημαίνει ότι η απόδηξη θα ειναι άκυρη αφού κάποιο όνομα προιόντος δεν θα έχει τελευταίο χαρακτήρα ":"
                            flag3=0   


################### Εδώ πρέπει το word πρέπει να περιέχει ακαίρεα ποσότητα προιόντος. flag4==0 σημαίνει ότι δεν έχουμε βρει την λέξη 'ΣΥΝΟΛΟ:' ακόμα 
                    elif (line_count>2 and word_count==2 and flag4==0):   
                        try:
                            tem1=int(word)
                        except:      
                            exc=1         # Η τιμή δεν είναι int. exc=1 Ακυρη η απόδηξη


################### Εδώ πρέπει το word πρέπει να περιέχει float τιμή προιόντος                           
                    elif (line_count>2 and word_count==3):
                        try:
                            tem2=float(word)       # Aποθηκεύω την τιμη στο temp2     
                        except:
                            exc=1                  # Η τιμή δεν είναι float. exc=1 Ακυρη η απόδηξη 


################### Εδώ πρέπει το word πρέπει να περιέχει float τιμή συνόλου γραμμής       
                    elif (line_count>2 and word_count==4):
                        try:
                            if ( (float(word)+0.001)>float(tem1*tem2) and (float(word)-0.001)<float(tem1*tem2) ): #Αν το σύνολο γρραμμής είναι σωστό σε 3ο δεκαδικό
                                total_prize=float(total_prize)+float(word)                                        #Κρατάω το συνολικό κόστος της απόδειξης ώς τώρα.
                            else:
                                total_prize="kati se string gia na vgalei exc=1 argotera kai na vgei akyrh h apodeiksh :)"  #Αν είναι λάθος το σύνολο μίας γραμής. 

                            timh_seiras=round(float(word),2)                   #πρωσπαθώ να πάρω float και άν το όνομα που είχε το προιόν της σειράς αυτής
                            if yparxei==1:                      
                                products[sthn_thesh+1]=round(products[sthn_thesh+1]+timh_seiras ,2)  # τότε η timh_seiras που περιέχει to word προστήθεται στην θέση +1 από την θέση
                            else:                                                                    # που υπάρχει το όνομα του προιόντος στην λιστα products[].
                                products.append(round(timh_seiras,2))   # Αλλιώς για πρώτη εμφάνηση ονομάτος η ποσότητα του μπαίνει δίπλα από το όνομα προιόντος στο τέλος της products[]
                            
                        except:
                            exc=1  # Η τιμή δεν είναι float. exc=1 Ακυρη η απόδηξη


################### Εδώ η λέξη word=='ΣΥΝΟΛΟ:' άρα βάζω flag4=1 για να ξέρω που φτάνει η ανάγνωση της απόδειξης.
                    elif (line_count>3 and word_count==1 and word=='ΣΥΝΟΛΟ:' and flag4==0):
                        flag4=1


################### Εδώ πρέπει το word να περιέχει float τιμή του συνολικού κόστους όλης της απόδειξης.
                    elif (line_count>3 and word_count==2 and flag4==1):
                        try:
                            if ( (float(word)+0.001)>total_prize and ((float(word)-0.001)<total_prize) ): #Αν η τιμή αυτή είναι ίση κατά 3ο δεκαδικό με το άθροισμα  
                                flag5=1                                                                   # κόστους όλων των γραμμών της απόδειξης, βάζω flag5=1.
                        except:
                            exc=1      # Η τιμή δεν είναι float. exc=1 Ακυρη η απόδηξη       

################################### Τέλος ελέγχων λέξεων ###########################################################################################################


############### Αν flagg==1 έχω βρεί δεύτερη σειρά με παύλες "---" άρα η απόδειξη έχει τελειώσει και πρέπει να αποφασίσω άν θα αποθηκεύσω τα temp_AFM και products[]
                    if (flagg==1):
                        
####################### Αν έχω όλα τα flags για έγκυρη απόδειξη τότε θα αποθηκεύσω τα temp_AFM και products[] στα diction και diction2 
                        if flag5==1 and flag4!=2 and format_flag==1 and flag==1 and flag2==1 and flag3==1 and flag_afm==10 and exc==0: 


########################### Αποθήκευση στο diction το οποίο έχει για key τα AΦΜ όπου temp_AFM το ΑΦΜ της τρέχον απόδειξης. 
                            if temp_AFM in diction:                         
                                dict_products=diction.get(temp_AFM)        # Aν το temp_AFM υπάρχει από προηγούμενη απόδειξη στο diction τότε σαρώνει για κάθε       
                                for i in range(0, len(products)-1, 2):     # προιόν στην products[] σε σχέση με κάθε με προιόν από το diction για key το temp_AFM.
                                    prod_exist=0
                                    for j in range(0, len(dict_products)-1, 2):
                                        if products[i]==dict_products[j]:                        # Όταν τα ονόματά τους είναι ίδια τότε προσθέτει στο diction την  
                                            dict_products[j+1]=dict_products[j+1]+products[i+1]  # ποσότητα του προιόντος από την products[]. Στην ανάλογη θέση 
                                            prod_exist=1                                         # δηλαδή της υπάρχουσας ποσότητας αυτού του προιόντος στο diction.
                                            break                                                # Και βάζω prod_exist=1 για να γνωρίζω ότι το προιόν υπάρχει.
                                    if prod_exist==0:              
                                        dict_products.append(products[i])   # Αν το προιόν αυτό από την products[] δεν υπάρχει στο diction για τό αφμ αυτό. 
                                        dict_products.append(products[i+1]) # Tότε προσθέτει αυτό και τις ποσότητες του στο τέλος της λίστας του diction για 
                                diction.update({temp_AFM:dict_products})    # key to temp_AFM
                            else:
                                diction.update({temp_AFM:products}) #Aλλιώς αν το temp_AFM δεν υπάρχει καν σαν key από προηγούμενη απόδειξη στο diction  
                                                                                               # τότε απλά το κάνω update την products για key το temp_AFM


########################### Αποθήκευση στο diction2 το οποίο έχει για key τα ονόματα προιόντων που περιέχονται στις ζυγές θέσεις της λίστας products[].                                 
                            for i in range(0, len(products)-1, 2):    
                                if products[i] in diction2:                 # Πρέπει για κλειδί το κάθε προιόν που υπάρχει στην τρέχον απόδειξη, δηλαδή στις ζυγές 
                                    dict2_values = diction2.get(products[i])   # θέσεις του products. Για κάθε τέτοιο products[i] ως key, ψάξε στο diction2 
                                    afm_existance=0
                                    for j in range(0, len(dict2_values)-1, 2): # όταν βρεις το τρέχον temp_AFM εκεί
                                        if dict2_values[j]==temp_AFM:
                                            dict2_values[j+1] = dict2_values[j+1]+products[i+1] # πρόσθεσε πόσα ακόμα από αυτό το προιόν πούλησε αυτό το αφμ
                                            diction2.update({products[i]:dict2_values})         # Έπειτα γίνoνται update τα νέα dict2_values για key to products[i]
                                            afm_existance=1

                                    if afm_existance==0:              # Αν αυτό το αφμ πρώτη φορά πουλάει αυτό το προιόν τότε απλα αποθηκεύεται στο τέλος του 
                                        dict2_values.append(temp_AFM)        # diction2 το αφμ και ή ποσότητα πούλησε. Όλα αυτά με κλειδί φυσικά το όνομα  
                                        dict2_values.append(products[i+1])             # προιόντος products[i].
                                        diction2.update({products[i]:dict2_values})        
                                else:
                                    list2=[]
                                    list2.append(temp_AFM)       # Στην περίπτωση οπου αυτό το προιόν products[i] δεν υπάρχει καν στο diction2, αυτό πάει να πεί ότι
                                    list2.append(products[i+1])           # δεν έχει πουληθεί από κανένα αφμ εώς τώρα. Τότε απλά για κλειδί το products[i] έχουμε
                                    diction2.update({products[i]:list2})  # μόνο το αφμ αυτής της απόδειξης temp_AFM, και την ποσότητα products[i+1] που αυτό έδωσε.


####################### Εδώ είτε έγκυρη είτε άκυρη η απόδειξη από την στιγμή που if (flagg==1): δίνω τις κατλάληλες τιμές΄ώστε να συνεχίσω στην ανάγνωση της επόμενης απόδειξης αφου έχω βρεί "-----".                         
                        line_count=1         # To line_count=1  και όχι ίσο με 0 . Αυτό αφου με την τελευταία σειρα "----" θεωρώ ότι έχω ήδη περάσει
                        flag=1               # την πρώτη σειρά της επόμενης απόδειξης. Εξού και flag=1
                        flagg=0              # όλα τα υπόλοιπα flags παίρνουν τις ίδιες τιμές που αρχικοποιήθηκαν πριν από την while True:
                        flag2=0              # Έτσι η επόμενη λούπα της while True: θα επεξεργάζεται την καινούρια απόδειξη.
                        flag3=1
                        flag4=0
                        flag5=0
                        flag_afm=0
                        total_prize=0
                        format_flag=1
                        products = []
                        exc=0

            file.close()    # Αφού διαβαστεί όλο το αρχείο. Έπειτα από το break της while True: κλείνω το αρχείο που είχα ανοιχτό και επιστρέφω  
                            # στην While true: εκτύπωσης του μενού και περιμένω επόμενη επιλογή από τον χρήστη.


### Για επιλογή χρήστη το 2 εκτυπώνω ταξινομεμένα τις στατιστικες με κλειδί τα προιόντα.
    elif choice == '2':
        product = input("Give Product Name: ")  # Ζητάω και παιρνω το όνομα προιόντος που δίνει ο χρήστης και το μεταφράζω σε κεφαλάια όπως όλα τα strings. 
        product = product.upper()               # Έχω έτοιμο αλλά αταξινόμητο το diction2. Έτσι για κλειδί το product που δίνει ο χρήστης αν υπάρχει στο diction2 τότε
        if product in diction2:                 
            products_afms=diction2.get(product) # περνάω την λίστα του diction2 για κλειδί το product, στο products_afms. Έπειτα γράφω τις ζηγές θέσεις του, δλδ τα ΑΦΜ
            products_afms_sorted=[]             # στην λίστα products_afms_sorted την οποία και ταξινομώ. Άρα αυτή περιέχει τα αφμ ταξινομιμένα.
            for i in products_afms[::2]:
                products_afms_sorted.append(i)
            products_afms_sorted=sorted(products_afms_sorted)
            for i in products_afms_sorted:                       # Έτσι με τη σειρά των ταξινομιμένων αφμ στην products_afms_sorted το κάθε τέτοιο αφμ 
                for j in range(0, len(products_afms)-1, 2):      # συχετίζεται με το ανάλογο αφμ της λίστας που στην επόμενη θέση υπάρχει η σωστή ποσότητα προιόντος.
                    if products_afms[j]==i:                      # Έτσι όταν ταυτίζοναι εκτυπώνεται αυτό το αφμ i, καθώς και η κατάληλη ποσότητά του που βρήσκεται 
                        print(i, round(products_afms[j+1],2))              # στην products_afms[j+1].



### Για επιλογή χρήστη το 3 εκτυπώνω ταξινομεμένα τις στατιστικές με κλειδί τα ΑΦΜ.           
    elif choice == '3':
        afm = input("Give AΦM: ")          # Ζητάω και παιρνω το ΑΜΦ που δίνει ο χρήστης
        if afm in diction:                 # Έχω έτοιμο αλλά αταξινόμητο το diction. Έτσι για κλειδί το ΑΦΜ που δίνει ο χρήστης αν αυτό υπάρχει στο diction2 τότε
            afm_prodacts=diction.get(afm)  # περνάω την λίστα του diction για κλειδί το afm, στο afm_prodacts. Έπειτα γράφω τις ζηγές θέσεις του, δλδ τα ονόματα 
            afm_prodacts_sorded=[]         # προιόντων στην λίστα afm_prodacts_sorded την οποία και ταξινομώ. Άρα αυτή περιέχει τα ονόματα προιόντων ταξινομιμένα.
            for i in afm_prodacts[::2]:        
                afm_prodacts_sorded.append(i)
            afm_prodacts_sorded=sorted(afm_prodacts_sorded)    
            for i in afm_prodacts_sorded:                      # Έτσι με τη σειρά των ταξινομιμένων ονομάτων προιόντων στην products_afms_sorted το κάθε τέτοιο όνομα
                for j in range(0, len(afm_prodacts)-1, 2):     # συχετίζεται με κάθε ανάλογο όνομα της λίστας που στην επόμενη θέση της έχει το σωστή ποσότητα για το 
                    if afm_prodacts[j]==i:                     # προιόν αυτό. Έτσι όταν αυτά ταυτίζοναι εκτυπώνεται αυτό το όνομα προιόντος i, καθώς και η κατάληλη 
                        print(i,round(afm_prodacts[j+1],2))             # ποσότητά του, που βρήσκετε στην afm_prodacts[j+1].

### Για επιλογή χρήστη το 4 το πρόγραμμα τερματίζει....!!!!!!!!!!!!!!!!!!!!!
    elif choice == '4':
        exit()
