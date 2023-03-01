# %%
# Import necessary modules
import numpy as np
import matplotlib.pyplot as plt
from PIL import Image
from os import listdir
from os.path import isfile, join
import pandas as pd
import time
import sys
from xgboost import XGBClassifier, plot_tree
from sklearn.model_selection import train_test_split
import streamlit as st
import pickle
import xgboost as xgb

# %%
def CSV_To_Desc(fichier):

    df = pd.read_csv(fichier, sep=",")
    st.write(df)
    if (df.shape[0] >= 59) :    
        print(df.shape)
        #print(fichier)
        df = df.drop(df.columns[[0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35]], axis=1, inplace=False)

        df=df[9:59]
        df.reset_index(drop=True, inplace=True)

        p = df.to_numpy()
        print(p.shape)

        des = []
        Desc = pd.DataFrame(columns =['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30', '31', '32', '33', '34', '35', '36', '37', '38', '39', '40', '41', '42', '43', '44', '45', '46', '47', '48', '49', '50', '51', '52', '53', '54', '55', '56', '57', '58', '59', '60', '61', '62', '63', '64', '65', '66', '67', '68', '69', '70', '71', '72', '73', '74', '75', '76', '77', '78', '79', '80', '81', '82', '83', '84', '85', '86', '87', '88', '89', '90', '91', '92', '93', '94', '95', '96', '97', '98', '99', '100', '101', '102', '103', '104', '105', '106', '107', '108', '109', '110', '111', '112', '113', '114', '115', '116', '117', '118', '119', '120', '121', '122', '123', '124', '125', '126', '127', '128', '129', '130', '131', '132', '133', '134', '135', '136', '137', '138', '139', '140', '141', '142', '143', '144', '145', '146', '147', '148', '149', '150', '151', '152', '153', '154', '155', '156', '157', '158', '159', '160', '161', '162', '163', '164', '165', '166', '167', '168', '169', '170', '171', '172', '173', '174', '175', '176', '177', '178', '179', '180', '181', '182', '183', '184', '185', '186', '187', '188', '189', '190', '191', '192', '193', '194', '195', '196', '197', '198', '199', '200', '201', '202', '203', '204', '205', '206', '207', '208', '209', '210', '211', '212', '213', '214', '215', '216', '217', '218', '219', '220', '221', '222', '223', '224', '225', '226', '227', '228', '229', '230', '231', '232', '233', '234', '235', '236', '237', '238', '239', '240', '241', '242', '243', '244', '245', '246', '247', '248', '249', '250', '251', '252', '253', '254', '255', '256', '257', '258', '259', '260', '261', '262', '263', '264', '265', '266', '267', '268', '269', '270', '271', '272', '273', '274', '275', '276', '277', '278', '279', '280', '281', '282', '283', '284', '285', '286', '287', '288', '289', '290', '291', '292', '293', '294', '295', '296', '297', '298', '299', '300', '301', '302', '303', '304', '305', '306', '307', '308', '309', '310', '311', '312', '313', '314', '315', '316', '317', '318', '319', '320', '321', '322', '323', '324', '325', '326', '327', '328', '329', '330', '331', '332', '333', '334', '335', '336', '337', '338', '339', '340', '341', '342', '343', '344', '345', '346', '347', '348', '349', '350', '351', '352', '353', '354', '355', '356', '357', '358', '359', '360', '361', '362', '363', '364', '365', '366', '367', '368', '369', '370', '371', '372', '373', '374', '375', '376', '377', '378', '379', '380', '381', '382', '383', '384', '385', '386', '387', '388', '389', '390', '391'])

        #des = np.array([])
        print(des)
        for i in range(0, p.shape[0]-1):
            for j in range(3, p.shape[1]+1, 3):
                des.append(np.linalg.norm(p[i+1][j-3:j] - p[i][j-3:j]))
                #des = np.append(des, np.linalg.norm(p[i+1][j-3:j] - p[i][j-3:j]))

    Desc.loc[len(Desc.index)] = des
    print(len(Desc))
    
    return Desc

# %%
def main():

    html_temp = """
    <div style="background-color:#025246 ;padding:10px">
    <h2 style="color:white;text-align:center;"> ANALYSE DE LA MARCHE HUMAINE</h2>
    </div>
    """
    st.markdown(html_temp, unsafe_allow_html=True)

         
    input = str(sys.argv[2])
    result = CSV_To_Desc(input)
    
    if st.button("Predict"):
        print("zzzz")
        xgb = pickle.load(open(r"Assets/ScriptPython/xgboost.sav", "rb"))


        output = xgb.predict(result)
        with st.spinner('Wait for it...'):
            time.sleep(3)
        st.success('Elle appartient à la classe : {}'.format(output[0]))
        st.balloons()
        if output[0] == 1: 
            image1 = Image.open(r"Assets/ScriptPython/classe1.PNG")
            st.image(image1, caption='Gait Classe 1', use_column_width ='always', output_format="PNG")

        elif output[0] == 2:
            image2 = Image.open(r"Assets/ScriptPython/classe2.PNG")
            st.image(image2, caption='Gait Classe 2', use_column_width ='always', output_format="PNG")
            st.subheader("Caractéristiques Pathologiques")
            st.caption("Essayez de maintenir le poids sur la jambe blessée pour éviter la douleur, raccourcir la phase de la marche de la jambe blessée.")
            st.subheader("Causes de l'anomalie ")
            st.caption("Douleur au pied, à la cheville, au genou ou à la hanche.")
        elif output[0] == 3:
            image3 = Image.open(r"Assets/ScriptPython/classe3.PNG")
            st.image(image3, caption='Gait Classe 3', use_column_width ='always', output_format="PNG")
            st.subheader("Caractéristiques Pathologiques")
            st.caption("Faire basculer le tronc vers l'arrière lors de la frappe du talon de la jambe blessée pour compenser la faiblesse de l'extension de la hanche.")
            st.subheader("Causes de l'anomalie ")
            st.caption("Faiblesse ou paralysie du muscle grand fessier.")
        elif output[0] == 4:
            image4 = Image.open(r"Assets/ScriptPython/classe4.PNG")
            st.image(image4, caption='Gait Classe 4', use_column_width ='always', output_format="PNG")
            st.subheader("Caractéristiques Pathologiques")
            st.caption("Problème de dorsiflexion dans la jambe blessée, soulevant la jambe blessée plus haut que la normale pour empêcher les orteils de racler le sol.")
            st.subheader("Causes de l'anomalie ")
            st.caption("Faiblesse ou paralysie du muscle tibial antérieur.")        
        elif output[0] == 5: 
            image5 = Image.open(r"Assets/ScriptPython/classe5.PNG")
            st.image(image5, caption='Gait Classe 5', use_column_width ='always', output_format="PNG")
            st.subheader("Caractéristiques Pathologiques")
            st.caption("Raideur de la jambe blessée en marchant, faisant un demi-cercle vers l'extérieur tout en balançant la jambe blessée.")
            st.subheader("Causes de l'anomalie ")
            st.caption("Pathologies liées aux articulations, telles que la polyarthrite rhumatoïde.")        
        elif output[0] == 6: 
            image6 = Image.open(r"Assets/ScriptPython/classe6.PNG")
            st.image(image6, caption='Gait Classe 6', use_column_width ='always', output_format="PNG")
            st.subheader("Caractéristiques Pathologiques")
            st.caption("Déplacez la hanche blessée vers le haut et l'autre hanche vers le bas pendant la phase d'appui pour équilibrer le niveau de la hanche, en faisant basculer le tronc vers le côté problématique.")
            st.subheader("Causes de l'anomalie ")
            st.caption("Faiblesse ou paralysie du moyen fessier et du petit fessier.")

# %%
print(xgb.__version__)



# %%
main()
# %%
