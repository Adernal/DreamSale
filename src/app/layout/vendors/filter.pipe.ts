import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter',
  pure:false,
})

export class FilterPipe implements PipeTransform {
  prop1:string;
  prop2:string;
  transform(value: any, filteredVendor: string ,propName :string,propName2:string): any {
    if(value.length===0 || filteredVendor===''){
      return value;
    }
    // this.prop1 = propName[0];
    // this.prop2 =propName[1];
    const resultArray=[];
    for(const item of value){
        if((item[propName].toLowerCase().indexOf(filteredVendor.toLowerCase())>=0) || (item[propName2].toLowerCase().indexOf(filteredVendor.toLowerCase())>=0)){
          resultArray.push(item);
        }

    }
    return resultArray;
  }

}
