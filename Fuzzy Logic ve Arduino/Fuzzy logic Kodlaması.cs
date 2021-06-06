#include "fis_header.h"

// Buradaki deger fuzzy logic için kaç girdiye sahipse o yazılıyor.
const int fis_gcI = 3;
//Kaç adet çıktı olacak
const int fis_gcO = 1;
// Girdi ve çıktılar kullanılarak oluşturulan kural tablolarının sayısı.
const int fis_gcR = 10;

FIS_TYPE g_fisInput[fis_gcI];
FIS_TYPE g_fisOutput[fis_gcO];

// Setup routine runs once when you press reset:
void setup()
{
    // initialize the Analog pins for input.


    // initialize the Analog pins for output.

}

// Loop routine runs over and over again forever:
void loop()
{


    fis_evaluate();


}

// Support functions for Fuzzy Inference System  
FIS_TYPE fis_array_operation(FIS_TYPE *array, int size, _FIS_ARR_OP pfnOp)
{
    int i;
    FIS_TYPE ret = 0;

    if (size == 0) return ret;
    if (size == 1) return array[0];

    ret = array[0];
    for (i = 1; i < size; i++)
    {
        ret = (*pfnOp)(ret, array[i]);
    }

    return ret;
}

// Data for Fuzzy Inference System 
// Pointers to the implementations of member functions
_FIS_MF fis_gMF[] =
{
    //girdi çıktılar için kullanılacak trimf trampf gibi seçenek belirtilir
};

// Her bir girdi için üye fonksiyon sayısı
int fis_gIMFCount[] = { };

// Her bir çıktı için üye fonksiyon sayısı 
int fis_gOMFCount[] = { };

// Giriş Üye Fonksiyonları için Katsayılar

FIS_TYPE** fis_gMFICoeff[] = { };

// Çıkış Üye Fonksiyonları için Katsayılar


FIS_TYPE** fis_gMFOCoeff[] = { };

// Giriş üyelik işlevi kümesi

int* fis_gMFI[] = { };

// Çıkış üyelik işlevi ayarlanma

int* fis_gMFO[] = {};

// Rule Weights
FIS_TYPE fis_gRWeight[] = { };

// Rule Type
int fis_gRType[] = { };

// Rule Inputs

int* fis_gRI[] = { };

// Rule Outputs

int* fis_gRO[] = { };

// Input range Min
FIS_TYPE fis_gIMin[] = { };

// Input range Max
FIS_TYPE fis_gIMax[] = { };

// Output range Min
FIS_TYPE fis_gOMin[] = { };

// Output range Max
FIS_TYPE fis_gOMax[] = { };

// Data dependent support functions for Fuzzy Inference System 
FIS_TYPE fis_MF_out(FIS_TYPE** fuzzyRuleSet, FIS_TYPE x, int o)
{
    FIS_TYPE mfOut;
    int r;

    for (r = 0; r < fis_gcR; ++r)
    {
        int index = fis_gRO[r][o];
        if (index > 0)
        {
            index = index - 1;
            mfOut = (fis_gMF[fis_gMFO[o][index]])(x, fis_gMFOCoeff[o][index]);
        }
        else if (index < 0)
        {
            index = -index - 1;
            mfOut = 1 - (fis_gMF[fis_gMFO[o][index]])(x, fis_gMFOCoeff[o][index]);
        }
        else
        {
            mfOut = 0;
        }

        fuzzyRuleSet[0][r] = fis_(mfOut, fuzzyRuleSet[1][r]);
    }
    return fis_array_operation(fuzzyRuleSet[0], fis_gcR, fis_);
}



// Fuzzy Inference System 
void fis_evaluate()
{

    FIS_TYPE* fuzzyInput[fis_gcI] = { };

    FIS_TYPE* fuzzyOutput[fis_gcO] = { };
    FIS_TYPE fuzzyRules[fis_gcR] = { 0 };
    FIS_TYPE fuzzyFires[fis_gcR] = { 0 };
    FIS_TYPE* fuzzyRuleSet[] = { fuzzyRules, fuzzyFires };
    FIS_TYPE sW = 0;

    // Transforming input to fuzzy Input
    int i, j, r, o;
    for (i = 0; i < fis_gcI; ++i)
    {
        for (j = 0; j < fis_gIMFCount[i]; ++j)
        {
            fuzzyInput[i][j] =
                (fis_gMF[fis_gMFI[i][j]])(g_fisInput[i], fis_gMFICoeff[i][j]);
        }
    }

    int index = 0;
    for (r = 0; r < fis_gcR; ++r)
    {
        if (fis_gRType[r] == 1)
        {
            fuzzyFires[r] = ;
            for (i = 0; i < fis_gcI; ++i)
            {
                index = fis_gRI[r][i];
                if (index > 0)
                    fuzzyFires[r] = fis_(fuzzyFires[r], fuzzyInput[i][index - 1]);
                else if (index < 0)
                    fuzzyFires[r] = fis_(fuzzyFires[r], 1 - fuzzyInput[i][-index - 1]);
                else
                    fuzzyFires[r] = fis_(fuzzyFires[r], 1);
            }
        }
        else
        {
            fuzzyFires[r] = ;
            for (i = 0; i < fis_gcI; ++i)
            {
                index = fis_gRI[r][i];
                if (index > 0)
                    fuzzyFires[r] = fis_(fuzzyFires[r], fuzzyInput[i][index - 1]);
                else if (index < 0)
                    fuzzyFires[r] = fis_(fuzzyFires[r], 1 - fuzzyInput[i][-index - 1]);
                else
                    fuzzyFires[r] = fis_(fuzzyFires[r], 0);
            }
        }

        fuzzyFires[r] = fis_gRWeight[r] * fuzzyFires[r];
        sW += fuzzyFires[r];
    }

    if (sW == 0)
    {
        for (o = 0; o < fis_gcO; ++o)
        {
            g_fisOutput[o] = ((fis_gOMax[o] + fis_gOMin[o]) / 2);
        }
    }
    else
    {
        for (o = 0; o < fis_gcO; ++o)
        {
/*FIS_SystemImpl*/
        }
    }
}